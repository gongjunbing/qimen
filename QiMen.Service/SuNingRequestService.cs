using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CommonUtil;
using Newtonsoft.Json.Linq;
using QiMen.Common;
using QiMen.DTOModel;

namespace QiMen.Service
{
    /// <summary>
    /// 苏宁接口实现
    /// </summary>
    public class SuNingRequestService : BaseRequestService
    {
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
            return bizParams;
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
        /// <param name="serverUrl"></param>
        /// <param name="apiKey"></param>
        /// <param name="headerParams"></param>
        /// <param name="bodyParams"></param>
        /// <returns></returns>
        public override string BuildRequestUrl(string serverUrl, string apiKey, QiMenDictionary headerParams, QiMenDictionary bodyParams)
        {
            return $"{serverUrl}/{apiKey}";
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

                if (item.Name == "appMethod")
                {
                    systemParams.Add("appMethod", apiKey);
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
        /// <param name="json"></param>
        /// <param name="objectId"></param>
        /// <param name="responeCode"></param>
        /// <returns></returns>
        public override IBaseMapDTO<JObject> MappingCalculation(string json, long objectId, string responeCode)
        {
            return null;
            //throw new NotImplementedException();
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
            string appRequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            QiMenDictionary headerParams = new QiMenDictionary
            {
                { "AppRequestTime", appRequestTime },
                { "AppKey", appKey },
                { "format","json" },
            };

            string appMethod = string.Empty;
            systemParams.TryGetValue("appMethod", out appMethod);
            headerParams.Add("AppMethod", appMethod);

            string versionNo = string.Empty;
            systemParams.TryGetValue("versionNo", out versionNo);
            headerParams.Add("VersionNo", versionNo);

            string accessToken = string.Empty;
            systemParams.TryGetValue("access_token", out accessToken);
            headerParams.Add("access_token", accessToken);

            //版本号为v1.2签名方式： 
            //1.业务数据进行base64加密
            //2.按照顺序依次拼接appSecret的值(appKey对应的密钥)，appMethod的值，appRequestTime的值，appkey的值，versionNo的值和第一步加密后的值
            //3.将第二步的数据进行md5加密，得到签名信息
            QiMenDictionary txtParams = new QiMenDictionary
            {
                { "appSecret", appSecret },
                { "AppMethod", appMethod },
                { "AppRequestTime", appRequestTime },
                { "AppKey", appKey },
                { "VersionNo", versionNo }
            };
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string item in txtParams.Values)
            {
                stringBuilder.Append(item);
            }
            string base64Value = SecurityUtil.EncodeBase64("{\"sn_request\":{\"sn_body\":{\"item\":{\"startTime\":\"2013-11-26 01:01:01\",\"endTime\":\"2013-12-25 23:59:59\",\"brandCode\":\"000C\",\"categoryCode\":\"R2701002\",\"status\":\"2\",\"pageNo\":\"1\",\"pageSize\":\"2\"}}}}");

            stringBuilder.Append(base64Value);

            byte[] array = SecurityUtil.MD5Encrypt(stringBuilder.ToString());
            StringBuilder signBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                signBuilder.Append(array[i].ToString("x2"));
            }

            string sign = signBuilder.ToString();
            headerParams.Add("signInfo", sign);

            return headerParams;
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
            string text = null;
            HttpWebResponse httpWebResponse = null;

            if (requestUrl.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            }
            HttpWebRequest httpWebRequest = null;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "text/html;charset=utf-8";
            httpWebRequest.Timeout = 30000;
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.KeepAlive = false;
            httpWebRequest.UserAgent = "suning-sdk-net-beta0.1";

            foreach (KeyValuePair<string, string> item in headerParams)
            {
                httpWebRequest.Headers.Add(item.Key, item.Value);
            }

            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("{\"sn_request\":{\"sn_body\":{\"item\":{\"startTime\":\"2013-11-26 01:01:01\",\"endTime\":\"2013-12-25 23:59:59\",\"brandCode\":\"000C\",\"categoryCode\":\"R2701002\",\"status\":\"2\",\"pageNo\":\"1\",\"pageSize\":\"2\"}}}}");
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
                streamReader.Close();
            }

            return text;
        }

        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
