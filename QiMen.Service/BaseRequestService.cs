using System.Collections.Generic;
using CommonUtil.Web;
using Newtonsoft.Json.Linq;
using QiMen.Common;
using QiMen.DTOModel;

namespace QiMen.Service
{
    /// <summary>
    /// 基础请求类，存放一些通用的请求参数。
    /// </summary>
    public abstract class BaseRequestService : QiMen.IService.IRequestService
    {
        /// <summary>
        /// 
        /// </summary>
        protected WebUtil _webUtil;

        /// <summary>
        /// 
        /// </summary>
        public BaseRequestService()
        {
            _webUtil = new WebUtil();
        }

        /// <summary>
        /// 生成业务入参
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="bizRequestDTO"></param>
        /// <returns></returns>
        public abstract QiMenDictionary BuildBizParams(QiMenDictionary parameters, List<QmApiBizRequestDTO> bizRequestDTO);
        /// <summary>
        /// 生成请求体
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="systemParams"></param>
        /// <param name="bizParams"></param>
        /// <returns></returns>
        public abstract QiMenDictionary BuildBodyParams(string appKey, string appSecret, QiMenDictionary systemParams, QiMenDictionary bizParams);
        /// <summary>
        /// 生成请求头部
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="systemParams"></param>
        /// <param name="bizParams"></param>
        /// <returns></returns>
        public abstract QiMenDictionary BuildHeaderParams(string appKey, string appSecret, QiMenDictionary systemParams, QiMenDictionary bizParams);
        /// <summary>
        /// 生成请求地址
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="apiKey"></param>
        /// <param name="headerParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        public abstract string BuildRequestUrl(string serverUrl, string apiKey, QiMenDictionary headerParams, QiMenDictionary bodyParams);
        /// <summary>
        /// 生成系统入参
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="apiKey"></param>
        /// <param name="parameters"></param>
        /// <param name="systemParamsDTO"></param>
        /// <returns></returns>
        public abstract QiMenDictionary BuildSystemParams(string appKey, string appSecret, string apiKey, QiMenDictionary parameters, List<QmAppSystemParamsDTO> systemParamsDTO);
        /// <summary>
        /// 发起请求
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="requestUrl"></param>
        /// <param name="headerParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        public abstract string DoExecute(string httpMethod, string requestUrl, QiMenDictionary headerParams, QiMenDictionary bodyParams);
        /// <summary>
        /// 映射算法
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objectId"></param>
        /// <param name="responeCode"></param>
        /// <returns></returns>
        public abstract IBaseMapDTO<JObject> MappingCalculation(string json, long objectId, string responeCode);
    }
}
