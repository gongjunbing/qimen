using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil;
using CommonUtil.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QiMen.Common;
using QiMen.DTOModel;

namespace QiMen.Service
{
    /// <summary>
    /// 淘宝接口实现
    /// </summary>
    public class TaoBaoRequestService : BaseRequestService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="apiKey"></param>
        /// <param name="headerParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        public override string BuildRequestUrl(string serverUrl, string apiKey, QiMenDictionary headerParams, QiMenDictionary bodyParams)
        {
            return serverUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="apiKey"></param>
        /// <param name="parameters"></param>
        /// <param name="systemParamsDTO"></param>
        /// <returns></returns>
        public override QiMenDictionary BuildSystemParams(string appKey, string appSecret, string apiKey, QiMenDictionary parameters, List<QmAppSystemParamsDTO> systemParamsDTO)
        {
            //指向平台请求入参
            QiMenDictionary systemParams = new QiMenDictionary();
            //从请求入参中取出匹配的系统入参
            foreach (QmAppSystemParamsDTO item in systemParamsDTO)
            {
                bool isContainsKey = parameters.ContainsKey(item.Name);
                string value = isContainsKey ? parameters[item.Name].ToStringEx()
                                             : !item.DefaultValue.IsNullOrEmpty() ? item.DefaultValue
                                                                                  : string.Empty;

                //if (item.IsRequired)
                //{
                //    if (value.IsNullOrEmpty())
                //    {
                //        return Error($"{item.Name}不可为空");
                //    }
                //}

                if (item.Name == "method")
                {
                    systemParams.Add("method", apiKey);
                    continue;
                }

                if (item.Name == "app_key")
                {
                    systemParams.Add("app_key", appKey);
                    continue;
                }

                if (item.Name == "sign_method")
                {
                    systemParams.Add("sign_method", "md5");
                    continue;
                }

                if (item.Name == "timestamp")
                {
                    systemParams.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    continue;
                }

                if (item.Name == "format")
                {
                    systemParams.Add("format", "json");
                    continue;
                }

                if (item.Name == "v")
                {
                    systemParams.Add("v", "2.0");
                    continue;
                }

                if (!value.IsNullOrEmpty())
                {
                    systemParams.Add(item.Name, value);
                }
            }

            return systemParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="bizRequestDTO"></param>
        /// <returns></returns>
        public override QiMenDictionary BuildBizParams(QiMenDictionary parameters, List<QmApiBizRequestDTO> bizRequestDTO)
        {
            //指向平台请求入参
            QiMenDictionary bizParams = new QiMenDictionary();
            //从请求入参中取出匹配的业务入参
            foreach (QmApiBizRequestDTO item in bizRequestDTO)
            {
                bool isContainsKey = parameters.ContainsKey(item.Name);
                string value = isContainsKey ? parameters[item.Name].ToStringEx()
                                             : !item.DefaultValue.IsNullOrEmpty() ? item.DefaultValue
                                                                                  : string.Empty;

                //if (item.IsRequired)
                //{
                //    if (value.IsNullOrEmpty())
                //    {
                //        return Error($"{item.Name}不可为空");
                //    }
                //}

                if (!value.IsNullOrEmpty())
                {
                    bizParams.Add(item.Name, value);
                }
            }

            return bizParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="systemParams"></param>
        /// <param name="bizParams"></param>
        /// <returns></returns>
        public override QiMenDictionary BuildBodyParams(string appKey, string appSecret, QiMenDictionary systemParams, QiMenDictionary bizParams)
        {
            QiMenDictionary txtParams = new QiMenDictionary();
            txtParams.AddAll(systemParams);
            txtParams.AddAll(bizParams);

            string sign = SecurityUtil.SignRequest(txtParams, appSecret);
            txtParams.Add("sign", sign);
            return txtParams;

        }

        /// <summary>
        /// 接口返回参数映射自定义对象算法
        /// </summary>
        /// <param name="json">接口返回参数json字符串</param>
        /// <param name="apiId">接口Id</param>
        /// <param name="responeCode">返回结果关键字</param>
        /// <returns></returns>
        public override IBaseMapDTO<JObject> MappingCalculation(string json, long apiId, string responeCode)
        {
            var result = new TaoBaoMapDTO<JObject>();
            if (json.IsNullOrEmpty())
            {
                result.ErrorCode = "500";
                result.ErrorMsg = "应用接口返回结果为空";
                return result;
            }

            if (apiId <= 0)
            {
                result.ErrorCode = "500";
                result.ErrorMsg = "不存在的接口ID";
                return result;
            }

            QmApiService qmApiService = new QmApiService();
            var apiModel = qmApiService.GetById(apiId);

            if (apiModel == null || apiModel.IsDelete)
            {
                result.ErrorCode = "500";
                result.ErrorMsg = "不存在的接口";
                return result;
            }

            QmObjectService qmObjectService = new QmObjectService();
            var objectModel = qmObjectService.GetById(apiModel.ObjectId);

            if (objectModel == null || objectModel.IsDelete)
            {
                result.OriginalBody = json;
                result.Body = null;
                return result;
            }

            TaoBaoTopResponse topResponse = JsonConvert.DeserializeObject<TaoBaoTopResponse>(json);
            if (topResponse.IsError())
            {
                result.ErrorCode = topResponse.ErrorResponse.ErrCode;
                result.ErrorMsg = topResponse.ErrorResponse.ErrMsg;
                result.ErrorSubCode = topResponse.ErrorResponse.SubErrCode;
                result.ErrorSubMsg = topResponse.ErrorResponse.SubErrMsg;
                result.RequestId = topResponse.ErrorResponse.RequestId;
                return result;
            }

            var joJson = JObject.Parse(json);
            var successKey = responeCode.Split('.').ToList();
            successKey.RemoveAt(0);
            var responeKey = string.Join('_', successKey) + "_response";
            var successBody = qmObjectService.GetMappingModel(joJson[responeKey], objectModel, apiModel);

            result.Body = successBody;
            result.OriginalBody = json;
            result.RequestId = "";
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="requestUrl"></param>
        /// <param name="headerParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        public override string DoExecute(string httpMethod, string requestUrl, QiMenDictionary headerParams, QiMenDictionary bodyParams)
        {
            return httpMethod.ToUpper() == Constants.HTTP_METHOD_POST ? _webUtil.DoPost(requestUrl, bodyParams) : _webUtil.DoGet(requestUrl, bodyParams);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="systemParams"></param>
        /// <param name="bizParams"></param>
        /// <returns></returns>
        public override QiMenDictionary BuildHeaderParams(string appKey, string appSecret, QiMenDictionary systemParams, QiMenDictionary bizParams)
        {
            return null;
        }
    }
}
