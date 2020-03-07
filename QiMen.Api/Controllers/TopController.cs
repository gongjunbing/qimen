using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QiMen.Models;
using QiMen.IService;
using QiMen.DbModel;
using CommonUtil;
using QiMen.Common;
using QiMen.Service;
using CommonUtil.Web;
using QiMen.DTOModel;
using EasyCaching.Core;

namespace QiMen.Api.Controllers
{
    /// <summary>
    /// 请求
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TopController : BaseController
    {
        /// <summary>
        /// 映射
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 奇门应用表操作接口
        /// </summary>
        private readonly IQmAppService _qmAppService;

        /// <summary>
        /// 奇门对象表操作接口
        /// </summary>
        private readonly IQmObjectService _qmObjectService;

        /// <summary>
        /// 奇门用户应用关系表操作接口
        /// </summary>
        private readonly IQmUserAppRelationService _qmUserAppRelationService;

        /// <summary>
        /// 奇门接口表操作接口
        /// </summary>
        private readonly IQmApiService _qmApiService;

        /// <summary>
        /// 应用系统入参表操作接口
        /// </summary>
        private readonly IQmAppSystemParamsService _qmAppSystemParamsService;

        /// <summary>
        /// 应用业务入参表操作接口
        /// </summary>
        private readonly IQmApiBizRequestService _qmApiBizRequestService;

        /// <summary>
        /// 指向请求接口
        /// </summary>
        private IRequestService _requestService;

        /// <summary>
        /// 缓存容器
        /// </summary>
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="qmAppService"></param>
        /// <param name="qmUserAppRelationService"></param>
        /// <param name="qmApiService"></param>
        /// <param name="qmAppSystemParamsService"></param>
        /// <param name="qmApiBizRequestService"></param>
        /// <param name="qmObjectService"></param>
        /// <param name="easyCachingProvider"></param>
        public TopController(IMapper mapper, IQmAppService qmAppService, IQmUserAppRelationService qmUserAppRelationService, IQmApiService qmApiService,
            IQmAppSystemParamsService qmAppSystemParamsService, IQmApiBizRequestService qmApiBizRequestService, IQmObjectService qmObjectService, IEasyCachingProvider easyCachingProvider)
        {
            this._mapper = mapper;
            this._qmAppService = qmAppService;
            this._qmUserAppRelationService = qmUserAppRelationService;
            this._qmApiService = qmApiService;
            this._qmAppSystemParamsService = qmAppSystemParamsService;
            this._qmApiBizRequestService = qmApiBizRequestService;
            this._qmObjectService = qmObjectService;
            this._provider = easyCachingProvider;
        }

        /// <summary>
        /// 尝试从内存中获取，若没有则从委托中获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private T TryGetInMemory<T>(string cacheKey, Func<T> func)
        {
            string key = $"QIMEN_{cacheKey}";
            T t = default(T);

            if (!_provider.Exists(key))
            {
                t = func.Invoke();

                if (!t.IsNull())
                {
                    _provider.Set(key, t, TimeSpan.FromHours(2));
                }

                return t;
            }

            CacheValue<T> cacheResult = _provider.Get<T>(key);

            if (cacheResult.HasValue && !cacheResult.IsNull)
            {
                LogUtil.Trace($"{key}缓存中：{ cacheResult.Value.ObjectToJson()}");
                return cacheResult.Value;
            }

            t = func.Invoke();

            if (!t.IsNull())
            {
                _provider.Set(key, t, TimeSpan.FromHours(2));
            }

            return t;
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(string))]
        public IActionResult Execute(TopRequest request)
        {
            QmApp qmApp = TryGetInMemory(request.AppShortName, () =>
            {
                //查出具体指向平台
                var appWhere = ExpressionUtil.Create<QmApp>().And(j => j.IsDelete == false)
                                                               .And(j => j.ShortName == request.AppShortName)
                                                               .ToExpression();
                return _qmAppService.GetSingle(appWhere);
            });

            if (qmApp.IsNull())
            {
                return Error("app_name不存在，根据应用简称未查找到应用，应用不存在或应用已被删除");
            }

            _requestService = GetRequestService(qmApp.ShortName);

            if (_requestService.IsNull())
            {
                return Error("平台未接入奇门");
            }

            QmUserAppRelation qmUserAppRelation = TryGetInMemory($"{qmApp.Id}_{request.UserId}_{request.UserId}", () =>
            {
                //查出用户和平台的app信息
                var userAppWhere = ExpressionUtil.Create<QmUserAppRelation>().And(j => j.IsDelete == false)
                                                                             .And(j => j.UserId == request.UserId)
                                                                             .And(j => j.AppId == qmApp.Id)
                                                                             .And(j => j.ShopId == request.ShopId)
                                                                             .ToExpression();
                return _qmUserAppRelationService.GetSingle(userAppWhere);
            });

            if (qmUserAppRelation.IsNull())
            {
                return Error("根据用户id和店铺id未查找到应用信息");
            }


            QmApi qmApi = TryGetInMemory($"{qmApp.Id}_{request.ApiKey}", () =>
            {
                //查出api具体指向的环境，平台真实指向的key
                var apiWhere = ExpressionUtil.Create<QmApi>().And(j => j.IsDelete == false)
                                                             .And(j => j.QmApiKey == request.ApiKey)
                                                             .And(j => j.AppId == qmApp.Id)
                                                             .ToExpression();
                return _qmApiService.GetSingle(apiWhere);
            });

            if (qmApi.IsNull())
            {
                return Error("根据method和app_name未查找到接口信息，接口不存在或接口已被删除");
            }

            //请求入参集合
            QiMenDictionary parameters = new QiMenDictionary();
            var kvps = base.Request.Form.GetEnumerator();
            while (kvps.MoveNext())
            {
                parameters.Add(kvps.Current.Key, kvps.Current.Value);
            }

            //系统入参集合
            QiMenDictionary systemParams = new QiMenDictionary();
            //业务入参集合
            QiMenDictionary bizParams = new QiMenDictionary();


            List<QmAppSystemParamsDTO> systemParamsList = TryGetInMemory($"system_params_{qmApi.Id}", () =>
            {
                //查出平台所需系统入参
                var systemParamsWhere = ExpressionUtil.Create<QmAppSystemParams>().And(j => j.IsDelete == false)
                                                                                  .And(j => j.AppId == qmApp.Id)
                                                                                  .ToExpression();
                return _qmAppSystemParamsService.ListGet<QmAppSystemParamsDTO>(j => new QmAppSystemParamsDTO { Id = j.Id, Name = j.Name, IsRequired = j.IsRequired, DefaultValue = j.DefaultValue }, systemParamsWhere);
            });
            if (!systemParamsList.IsNull() && systemParamsList.Count > 0)
            {
                systemParams = _requestService.BuildSystemParams(qmUserAppRelation.AppKey, qmUserAppRelation.AppSecret, qmApi.OriginApiKey, parameters, systemParamsList);
            }


            List<QmApiBizRequestDTO> bizRequestList = TryGetInMemory($"biz_params_{qmApi.Id}", () =>
            {
                //查出平台所需业务入参
                var bizRequestWhere = ExpressionUtil.Create<QmApiBizRequest>().And(j => j.IsDelete == false)
                                                                              .And(j => j.ApiId == qmApi.Id)
                                                                              .ToExpression();
                return _qmApiBizRequestService.ListGet<QmApiBizRequestDTO>(j => new QmApiBizRequestDTO { Id = j.Id, Name = j.Name, IsRequired = j.IsRequired, DefaultValue = j.DefaultValue }, bizRequestWhere);
            });

            if (!bizRequestList.IsNull() && bizRequestList.Count > 0)
            {
                bizParams = _requestService.BuildBizParams(parameters, bizRequestList);
            }


            QiMenDictionary headerParams = _requestService.BuildHeaderParams(qmUserAppRelation.AppKey, qmUserAppRelation.AppSecret, systemParams, bizParams);
            QiMenDictionary bodyParams = _requestService.BuildBodyParams(qmUserAppRelation.AppKey, qmUserAppRelation.AppSecret, systemParams, bizParams);

            //根据接口是否上线确认接口环境
            string serverUrl = qmApi.IsOnline ? qmUserAppRelation.AppProductUrl : qmUserAppRelation.AppSandboxUrl;
            //根据不同平台规则生成请求URL
            string requestUrl = _requestService.BuildRequestUrl(serverUrl, qmApi.OriginApiKey, headerParams, bodyParams);

            string body;
            long start = DateTime.Now.Ticks;

            try
            {
                body = _requestService.DoExecute(qmApi.Method, requestUrl, headerParams, bodyParams);

                LogUtil.Trace(body);
            }
            catch (Exception e)
            {
                TimeSpan latency = new TimeSpan(DateTime.Now.Ticks - start);

                LogUtil.Error(e, $"请求毫秒TotalMilliseconds：{latency.TotalMilliseconds}^_^请求值request：{request.ObjectToJson()}^_^发起请求头部参数headerParams{bodyParams.ObjectToJson()}^_^发起请求参数bodyParams：{bodyParams.ObjectToJson()}");

                return Error(e.Message);
            }

            var result = _requestService.MappingCalculation(body, qmApi.Id, qmApi.OriginApiKey);

            return Success(result);
        }

        /// <summary>
        /// 确认指向平台
        /// </summary>
        /// <param name="appShortName"></param>
        /// <returns></returns>
        private IRequestService GetRequestService(string appShortName)
        {
            switch (appShortName.ToUpper())
            {
                case "TB":
                    return new TaoBaoRequestService();
                case "SN":
                    return new SuNingRequestService();
                case "COP":
                    return new COPRequestService();
                case "DM":
                    return new DMRequestService();
                default:
                    return null;
            }
        }
    }
}