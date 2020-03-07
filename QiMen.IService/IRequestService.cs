
using Newtonsoft.Json.Linq;
using QiMen.Common;
using QiMen.DTOModel;
using System.Collections.Generic;

namespace QiMen.IService
{
    /// <summary>
    /// 请求接口
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// 生成请求地址
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="apiKey"></param>
        /// <param name="headerParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        string BuildRequestUrl(string serverUrl, string apiKey, QiMenDictionary headerParams, QiMenDictionary bodyParams);

        /// <summary>
        /// 生成系统入参
        /// </summary>
        /// <param name="appKey">公钥</param>
        /// <param name="appSecret">私钥</param>
        /// <param name="apiKey"></param>
        /// <param name="parameters">请求入参</param>
        /// <param name="systemParamsDTO">系统入参配置</param>
        /// <returns></returns>
        QiMenDictionary BuildSystemParams(string appKey, string appSecret, string apiKey, QiMenDictionary parameters, List<QmAppSystemParamsDTO> systemParamsDTO);

        /// <summary>
        /// 生成业务入参
        /// </summary>
        /// <param name="parameters">域名</param>
        /// <param name="bizRequestDTO">业务请求入参</param>
        /// <returns></returns>
        QiMenDictionary BuildBizParams(QiMenDictionary parameters, List<QmApiBizRequestDTO> bizRequestDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="systemParams"></param>
        /// <param name="bizParams"></param>
        /// <returns></returns>
        QiMenDictionary BuildHeaderParams(string appKey, string appSecret, QiMenDictionary systemParams, QiMenDictionary bizParams);

        /// <summary>
        /// 生成请求体
        /// </summary>
        /// <param name="appKey">公钥</param>
        /// <param name="appSecret">私钥</param>
        /// <param name="systemParams">系统入参集合</param>
        /// <param name="bizParams">业务入参集合</param>
        /// <returns></returns>
        QiMenDictionary BuildBodyParams(string appKey, string appSecret, QiMenDictionary systemParams, QiMenDictionary bizParams);

        /// <summary>
        /// 发起请求
        /// </summary>
        /// <returns></returns>
        string DoExecute(string httpMethod, string requestUrl, QiMenDictionary headerParams, QiMenDictionary bodyParams);


        /// <summary>
        /// 映射算法
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objectId"></param>
        /// <param name="responeCode"></param>
        /// <returns></returns>
        IBaseMapDTO<JObject> MappingCalculation(string json, long objectId, string responeCode);
    }
}
