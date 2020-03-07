using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CommonUtil;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QiMen.Api.Attributes;
using QiMen.DbModel;
using QiMen.IService;
using QiMen.Models;
using QiMen.Service;
using SqlSugar;

namespace QiMen.Api.Controllers
{
    /// <summary>
    /// 奇门对象
    /// </summary>
    [Route(TopConstants.Service_API_ROUTE)]
    [FlushCache]
    public class QmSettingController : BaseController
    {
        private readonly IQmObjectService _qmObjectService;
        private readonly IQmObjectAttributesService _qmObjectAttributesService;
        private readonly IQmApiService _qmApiService;
        private readonly IQmApiBizRequestService _qmApiBizRequestService;
        private readonly IQmAppService _qmAppService;
        private readonly IQmAppSystemParamsService _qmAppSystemParamsService;
        private readonly IQmUserAppRelationService _qmUserAppRelationService;
        private readonly IQmResponeService _qmResponeService;


        /// <summary>
        /// 构造函数
        /// </summary>
        public QmSettingController(IQmObjectService qmObjectService, IQmObjectAttributesService qmObjectAttributesService, IQmApiService qmApiService, IQmApiBizRequestService qmApiBizRequestService, IQmAppService qmAppService, IQmAppSystemParamsService qmAppSystemParamsService, IQmUserAppRelationService qmUserAppRelationService, IQmResponeService qmResponeService)
        {
            _qmObjectService = qmObjectService;
            _qmObjectAttributesService = qmObjectAttributesService;
            _qmApiService = qmApiService;
            _qmApiBizRequestService = qmApiBizRequestService;
            _qmAppService = qmAppService;
            _qmAppSystemParamsService = qmAppSystemParamsService;
            _qmUserAppRelationService = qmUserAppRelationService;
            _qmResponeService = qmResponeService;
        }

        /// <summary>
        /// 获取应用集合
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(List<GetAppListRespone>))]
        public IActionResult GetAppList([FromBody]GetAppListRequest model)
        {
            var result = new List<GetAppListRespone>();
            var qmWhere = Expressionable.Create<QmApp>()
                .AndIF(!model.ShortName.IsNullOrEmpty(), a => a.ShortName == model.ShortName)
                .AndIF(!model.Name.IsNullOrEmpty(), a => a.Name == model.Name)
                .And(a => a.IsDelete == false).ToExpression();
            var total = 0;
            result=_qmAppService.ListPageGet(qmWhere, model.CurrentPage, model.PageSize, ref total).Select(a=>new GetAppListRespone() { AppId=a.Id,ShortName=a.ShortName,Name=a.Name,CreateTime=a.CreateTime}).ToList();

            return Success(result,model.CurrentPage,model.PageSize, total);
        }

        /// <summary>
        /// 获取接口集合
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(List<GetApiListRespone>))]
        public IActionResult GetApiList([FromBody]GetApiListRequest model)
        {
            var result = new List<GetApiListRespone>();

            var qmWhere = Expressionable.Create<QmApi>()
                .AndIF(model.AppId>0, a => a.AppId == model.AppId)
                .AndIF(!model.OriginApiKey.IsNullOrEmpty(), a => a.OriginApiKey == model.OriginApiKey)
                .AndIF(!model.QmApiKey.IsNullOrEmpty(), a => a.QmApiKey == model.QmApiKey)
                .And(a => a.IsDelete == false).ToExpression();
            var total = 0;
            result = _qmApiService.ListPageGet(qmWhere, model.CurrentPage, model.PageSize, ref total).Select(a => new GetApiListRespone() { AppId = a.AppId,
                ApiId=a.Id,
                QmApiKey=a.QmApiKey,
                OriginApiKey=a.OriginApiKey,
                Environment=a.Environment,
                IsOnline=a.IsOnline,
                IsSign=a.IsSign,
                Method=a.Method,
                ObjectId=a.ObjectId,
                CreateTime = a.CreateTime }).ToList();

            return Success(result, model.CurrentPage, model.PageSize, total);
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(List<GetObjectListRespone>))]
        public IActionResult GetObjectList([FromBody]GetObjectListRequest model)
        {
            var result = new List<GetObjectListRespone>();

            var qmWhere = Expressionable.Create<QmObject>()
                .AndIF(!model.ObjectName.IsNullOrEmpty(), a => a.ObjectName == model.ObjectName)
                .AndIF(!model.ObjectKey.IsNullOrEmpty(), a => a.ObjectKey == model.ObjectKey)
                .And(a => a.IsDelete == false).ToExpression();
            var total = 0;
            result = _qmObjectService.ListPageGet(qmWhere, model.CurrentPage, model.PageSize, ref total).Select(a => new GetObjectListRespone()
            {
                ObjectId=a.Id,
                ObjectDesc=a.ObjectDesc,
                ObjectKey=a.ObjectKey,
                ObjectName=a.ObjectName,
                CreateTime = a.CreateTime
            }).ToList();

            return Success(result, model.CurrentPage, model.PageSize, total);
        }

        /// <summary>
        /// 获取平台详细信息
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(GetAppDetailRespone))]
        public IActionResult GetAppDetail([FromBody]GetAppDetailRequest model)
        {
            var result = new GetAppDetailRespone();
            var app = _qmAppService.GetById(model.AppId);
            if (app==null||app.IsDelete)
            {
                return Error("应用不存在");
            }

            result.AppId = model.AppId;
            result.CreateTime = app.CreateTime;
            result.Name = app.Name;
            result.ShortName = app.ShortName;
            result.SystemParamList = _qmAppSystemParamsService.ListGet(a => a.IsDelete == false && a.AppId == model.AppId);
            result.UserList = _qmUserAppRelationService.ListGet(a => a.IsDelete == false && a.AppId == model.AppId);
            result.ApiList = _qmApiService.ListGet(a => a.AppId == model.AppId && a.IsDelete == false);

            return Success(result);
        }

        /// <summary>
        /// 新增平台
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddApp([FromBody]AddAppRequest model)
        {
            var result = false;

            var tranResult = _qmAppService.UseTran(() => {
                var app = new QmApp() {
                    Id=TopUtil.GetId(),
                    ShortName=model.ShortName,
                    Name=model.Name,
                    CreateTime=DateTime.Now
                };
                _qmAppService.Insert(app);

                foreach (var item in model.SystemParamList)
                {
                    var systemParam = new QmAppSystemParams() {
                        AppId=app.Id,
                        CreateTime=DateTime.Now,
                        DefaultValue=item.DefaultValue,
                        Example=item.Example,
                        Id=TopUtil.GetId(),
                        IsRequired=item.IsRequired,
                        Name=item.Name,
                        Type=item.Type
                    };
                    _qmAppSystemParamsService.Insert(systemParam);
                }
            });
            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 删除平台
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteApp([FromBody]DeleteAppRequest model)
        {
            var result = false;
            var tranResult = _qmAppService.UseTran(() => {
                _qmAppService.Update(a => new QmApp() { IsDelete = true }, a => a.Id == model.AppId);
                _qmAppSystemParamsService.Update(a => new QmAppSystemParams() { IsDelete = true }, a => a.AppId == model.AppId);
                _qmUserAppRelationService.Update(a => new QmUserAppRelation() { IsDelete = true }, a => a.AppId == model.AppId);
                _qmApiService.Update(a => new QmApi() { IsDelete = true }, a => a.AppId == model.AppId);
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 删除公共请求参数
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteSystemParam([FromBody]DeleteSystemParamRequest model)
        {
            var result = false;
            if (model.SystemParamId!=null&&model.SystemParamId.Count>0)
            {
                result=_qmAppSystemParamsService.Update(a => new QmAppSystemParams() { IsDelete = true }, a => model.SystemParamId.Contains(a.Id))>0;
            }

            if (!result)
            {
                return Error("删除失败");
            }

            return Success(result);
        }


        /// <summary>
        /// 新增公共请求参数
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddSystemParam([FromBody]AddSystemParamRequest model)
        {
            var result = false;

            var app = _qmAppService.GetById(model.AppId);
            if (app==null||app.IsDelete)
            {
                return Error("应用不存在");
            }

            var tranResult = _qmAppService.UseTran(() => {
                foreach (var item in model.SystemParamList)
                {
                    var systemParam = new QmAppSystemParams()
                    {
                        AppId = model.AppId,
                        CreateTime = DateTime.Now,
                        DefaultValue = item.DefaultValue,
                        Example = item.Example,
                        Id = TopUtil.GetId(),
                        IsRequired = item.IsRequired,
                        Name = item.Name,
                        Type = item.Type
                    };
                    _qmAppSystemParamsService.Insert(systemParam);
                }
            });
            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteUser([FromBody]DeleteUserRequest model)
        {
            var result = false;
            if (model.UserId != null && model.UserId.Count > 0)
            {
                result=_qmUserAppRelationService.Update(a => new QmUserAppRelation() { IsDelete = true }, a => model.UserId.Contains(a.Id))>0;
            }

            if (!result)
            {
                return Error("删除失败");
            }

            return Success(result);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddUser([FromBody]AddUserRequest model)
        {
            var result = false;

            var app = _qmAppService.GetById(model.AppId);
            if (app == null || app.IsDelete)
            {
                return Error("应用不存在");
            }

            var tranResult = _qmAppService.UseTran(() => {
                foreach (var item in model.UserList)
                {
                    var user = new QmUserAppRelation()
                    {
                        AppId = model.AppId,
                        CreateTime = DateTime.Now,
                        ShopId=item.ShopId,
                        AppSandboxUrl=item.AppSandboxUrl,
                        AppSecret=item.AppSecret,
                        AppKey=item.AppKey,
                        AppProductUrl=item.AppProductUrl,
                        Id=TopUtil.GetId(),
                        UserId=item.UserId
                    };
                    _qmUserAppRelationService.Insert(user);
                }
            });
            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 新增接口
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddApi([FromBody]AddApiRequest model)
        {
            var result = false;

            var tranResult = _qmApiService.UseTran(() => {
                var api = new QmApi() {
                    IsSign=model.IsSign,
                    AppId=model.AppId,
                    CreateTime=DateTime.Now,
                    Environment=model.Environment,
                    Id=TopUtil.GetId(),
                    IsOnline=model.IsOnline,
                    Method=model.Method,
                    ObjectId=model.ObjectId,
                    OriginApiKey=model.OriginApiKey,
                    QmApiKey=model.QmApiKey
                };
                _qmApiService.Insert(api);

                foreach (var item in model.ApiBizRequestList)
                {
                    var request = new QmApiBizRequest()
                    {
                        CreateTime = DateTime.Now,
                        Id = TopUtil.GetId(),
                        ApiId=api.Id,
                        DefaultValue=item.DefaultValue,
                        Desc=item.Desc,
                        Example=item.Example,
                        IsRequired=item.IsRequired,
                        MaxLength=item.MaxLength,
                        MaxValue=item.MaxValue,
                        MinLength=item.MinLength,
                        MinValue=item.MinValue,
                        Name=item.Name,
                        Type=item.Type
                    };
                    _qmApiBizRequestService.Insert(request);
                }

                foreach (var item in model.ApiResponeList)
                {
                    var respone = new QmRespone()
                    {
                        CreateTime = DateTime.Now,
                        Id = TopUtil.GetId(),
                        ApiId = api.Id,
                        Desc = item.Desc,
                        Name = item.Name,
                        Type = item.Type,
                        ObjectId=item.ObjectId,
                        AttributeKey=item.AttributeKey
                    };
                    _qmResponeService.Insert(respone);
                }
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 获取接口详细信息
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(GetApiDetailRespone))]
        public IActionResult GetApiDetail([FromBody]GetApiDetailRequest model)
        {
            var result = new GetApiDetailRespone();
            var api = _qmApiService.GetById(model.ApiId);
            if (api == null || api.IsDelete)
            {
                return Error("接口不存在");
            }

            result.ApiId = model.ApiId;
            result.CreateTime = api.CreateTime;
            result.AppId = api.AppId;
            result.Environment = api.Environment;
            result.IsOnline = api.IsOnline;
            result.IsSign = api.IsSign;
            result.OriginApiKey = api.OriginApiKey;
            result.QmApiKey = api.QmApiKey;
            result.ObjectId = api.ObjectId;
            result.Method = api.Method;
            result.RequestList = _qmApiBizRequestService.ListGet(a => a.IsDelete == false && a.ApiId == model.ApiId);
            result.ResponeList = _qmResponeService.ListGet(a => a.IsDelete == false && a.ApiId == model.ApiId);

            return Success(result);
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteApi([FromBody]DeleteApiRequest model)
        {
            var result = false;
            var tranResult = _qmAppService.UseTran(() => {
                _qmApiService.Update(a => new QmApi() { IsDelete = true }, a => a.Id == model.ApiId);
                _qmApiBizRequestService.Update(a => new QmApiBizRequest() { IsDelete = true }, a => a.ApiId == model.ApiId);
                _qmResponeService.Update(a => new QmRespone() { IsDelete = true }, a => a.ApiId == model.ApiId);
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 新增接口请求参数
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddApiRequest([FromBody]AddApiBizRequest model)
        {
            var result = false;

            var api = _qmApiService.GetById(model.ApiId);
            if (api==null|| api.IsDelete)
            {
                return Error("接口不存在");
            }

            var tranResult = _qmApiService.UseTran(() => {

                foreach (var item in model.ApiBizRequestList)
                {
                    var request = new QmApiBizRequest()
                    {
                        CreateTime = DateTime.Now,
                        Id = TopUtil.GetId(),
                        ApiId = api.Id,
                        DefaultValue = item.DefaultValue,
                        Desc = item.Desc,
                        Example = item.Example,
                        IsRequired = item.IsRequired,
                        MaxLength = item.MaxLength,
                        MaxValue = item.MaxValue,
                        MinLength = item.MinLength,
                        MinValue = item.MinValue,
                        Name = item.Name,
                        Type = item.Type
                    };
                    _qmApiBizRequestService.Insert(request);
                }
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 删除接口请求参数
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteApiRequest([FromBody]DeleteApiBizRequest model)
        {
            var result = false;
            if (model.RequestId != null && model.RequestId.Count > 0)
            {
                result=_qmApiBizRequestService.Update(a => new QmApiBizRequest() { IsDelete = true }, a => model.RequestId.Contains(a.Id))>0;
            }

            if (!result)
            {
                return Error("删除失败");
            }

            return Success(result);
        }

        /// <summary>
        /// 新增接口返回参数
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddApiRespone([FromBody]AddApiResponeRequest model)
        {
            var result = false;
            var api = _qmApiService.GetById(model.ApiId);
            if (api == null || api.IsDelete)
            {
                return Error("接口不存在");
            }

            var tranResult = _qmApiService.UseTran(() => {
                foreach (var item in model.ApiResponeList)
                {
                    var respone = new QmRespone()
                    {
                        CreateTime = DateTime.Now,
                        Id = TopUtil.GetId(),
                        ApiId = api.Id,
                        Desc = item.Desc,
                        Name = item.Name,
                        Type = item.Type,
                        ObjectId = item.ObjectId,
                        AttributeKey = item.AttributeKey
                    };
                    _qmResponeService.Insert(respone);
                }
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 删除接口返回参数
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteApiRespone([FromBody]DeleteApiResponeRequest model)
        {
            var result = false;
            if (model.ResponeId != null && model.ResponeId.Count > 0)
            {
                result = _qmResponeService.Update(a => new QmRespone() { IsDelete = true }, a => model.ResponeId.Contains(a.Id)) > 0;
            }

            if (!result)
            {
                return Error("删除失败");
            }

            return Success(result);
        }

        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddObject([FromBody]AddObjectRequest model)
        {
            var result = false;

            var tranResult = _qmObjectService.UseTran(() => {
                var qmObject = new QmObject()
                {
                    CreateTime = DateTime.Now,
                    Id = TopUtil.GetId(),
                    ObjectDesc=model.ObjectDesc,
                    ObjectKey=model.ObjectKey,
                    ObjectName=model.ObjectName
                };
                _qmObjectService.Insert(qmObject);

                foreach (var item in model.AttributesList)
                {
                    var attributes = new QmObjectAttributes()
                    {
                        CreateTime = DateTime.Now,
                        Id = TopUtil.GetId(),
                        ObjectId = qmObject.Id,
                        AttributesDesc=item.AttributesDesc,
                        AttributesKey=item.AttributesKey,
                        AttributesName=item.AttributesName,
                        IsList=item.IsList,
                        ObjectType=item.ObjectType,
                        RelationId=item.RelationId
                    };
                    _qmObjectAttributesService.Insert(attributes);
                }
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 获取对象详细信息
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(GetObjectDetailRespone))]
        public IActionResult GetObjectDetail([FromBody]GetObjectDetailRequest model)
        {
            var result = new GetObjectDetailRespone();
            var qmObject = _qmObjectService.GetById(model.ObjectId);
            if (qmObject == null || qmObject.IsDelete)
            {
                return Error("对象不存在");
            }

            result.ObjectId = model.ObjectId;
            result.CreateTime = qmObject.CreateTime;
            result.ObjectDesc = qmObject.ObjectDesc;
            result.ObjectKey = qmObject.ObjectKey;
            result.ObjectName = qmObject.ObjectName;
            result.AttributesList = _qmObjectAttributesService.ListGet(a => a.IsDelete == false && a.ObjectId == model.ObjectId);

            return Success(result);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteObject([FromBody]DeleteObjectRequest model)
        {
            var result = false;
            var tranResult = _qmObjectService.UseTran(() => {
                _qmObjectService.Update(a => new QmObject() { IsDelete = true }, a => a.Id == model.ObjectId);
                _qmObjectAttributesService.Update(a => new QmObjectAttributes() { IsDelete = true }, a => a.ObjectId == model.ObjectId);
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult AddAttribute([FromBody]AddAttributeRequest model)
        {
            var result = false;

            var qmObject = _qmObjectService.GetById(model.ObjectId);
            if (qmObject==null||qmObject.IsDelete)
            {
                return Error("对象不存在");
            }

            var tranResult = _qmObjectService.UseTran(() => {
                foreach (var item in model.AttributesList)
                {
                    var attributes = new QmObjectAttributes()
                    {
                        CreateTime = DateTime.Now,
                        Id = TopUtil.GetId(),
                        ObjectId = qmObject.Id,
                        AttributesDesc = item.AttributesDesc,
                        AttributesKey = item.AttributesKey,
                        AttributesName = item.AttributesName,
                        IsList = item.IsList,
                        ObjectType = item.ObjectType,
                        RelationId = item.RelationId
                    };
                    _qmObjectAttributesService.Insert(attributes);
                }
            });

            if (!tranResult.IsSuccess)
            {
                return Error(tranResult.ErrorMessage);
            }

            result = tranResult.IsSuccess;

            return Success(result);
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(bool))]
        public IActionResult DeleteAttribute([FromBody]DeleteAttributeRequest model)
        {
            var result = false;
            if (model.AttributesId != null && model.AttributesId.Count > 0)
            {
                result = _qmObjectAttributesService.Update(a => new QmObjectAttributes() { IsDelete = true }, a => model.AttributesId.Contains(a.Id)) > 0;
            }

            if (!result)
            {
                return Error("删除失败");
            }

            return Success(result);
        }
    }
}
