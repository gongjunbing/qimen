using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QiMen.DTOModel
{


    /// <summary>
    /// </summary>
    public class TaoBaoTopResponse : ITopResponse
    {
        /// <summary>
        /// 错误对象
        /// </summary>
        [JsonProperty(PropertyName = "error_response")]
        public TaoBaoErrorResponse ErrorResponse { get; set; }

        public bool IsError()
        {
            if (this.ErrorResponse == null)
            {
                return false;
            }
            else
            {
                return !string.IsNullOrEmpty(this.ErrorResponse.ErrCode) || !string.IsNullOrEmpty(this.ErrorResponse.SubErrCode);
            }
        }
    }

    public class TaoBaoErrorResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 子错误码
        /// </summary>
        [JsonProperty(PropertyName = "sub_code")]
        public string SubErrCode { get; set; }

        /// <summary>
        /// 子错误信息
        /// </summary>
        [JsonProperty(PropertyName = "sub_msg")]
        public string SubErrMsg { get; set; }

        /// <summary>
        /// 请求结果id
        /// </summary>
        [JsonProperty(PropertyName = "request_id")]
        public string RequestId { get; set; }
    }
}
