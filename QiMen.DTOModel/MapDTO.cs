using Newtonsoft.Json;

namespace QiMen.DTOModel
{

    /// <summary>
    /// 
    /// </summary>
    public interface IBaseMapDTO<T>
    {
        /// <summary>
        /// 平台颁发的每次请求访问的唯一标识
        /// </summary>
        [JsonProperty("request_id")]
         string RequestId { get; set; }

        /// <summary>
        /// 源数据
        /// </summary>
        [JsonProperty("original_body")]
         string OriginalBody { get; set; }

        /// <summary>
        /// 映射结果
        /// </summary>
        [JsonProperty("qimen_body")]
         T Body { get; set; }


        /// <summary>
        /// 请求失败返回的错误码
        /// </summary>
        [JsonProperty("error_code")]
         string ErrorCode { get; set; }

        /// <summary>
        /// 请求失败返回的错误信息
        /// </summary>
        [JsonProperty("error_msg")]
         string ErrorMsg { get; set; }
    }

    /// <summary>
    /// 映射成功模型
    /// </summary>
    public class TaoBaoMapDTO<T>: IBaseMapDTO<T>
    {
        /// <summary>
        /// 平台颁发的每次请求访问的唯一标识
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// 源数据
        /// </summary>
        [JsonProperty("original_body")]
        public string OriginalBody { get; set; }

        /// <summary>
        /// 映射结果
        /// </summary>
        [JsonProperty("qimen_body")]
        public T Body { get; set; }


        /// <summary>
        /// 请求失败返回的错误码
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 请求失败返回的错误信息
        /// </summary>
        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 请求失败返回的子错误码
        /// </summary>
        [JsonProperty("error_sub_code")]
        public string ErrorSubCode { get; set; }

        /// <summary>
        /// 请求失败返回的子错误信息
        /// </summary>
        [JsonProperty("error_sub_msg")]
        public string ErrorSubMsg { get; set; }
    }

    /// <summary>
    /// 映射成功模型
    /// </summary>
    public class COPMapDTO<T> : IBaseMapDTO<T>
    {
        /// <summary>
        /// 平台颁发的每次请求访问的唯一标识
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// 源数据
        /// </summary>
        [JsonProperty("original_body")]
        public string OriginalBody { get; set; }

        /// <summary>
        /// 映射结果
        /// </summary>
        [JsonProperty("qimen_body")]
        public T Body { get; set; }


        /// <summary>
        /// 请求失败返回的错误码
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 请求失败返回的错误信息
        /// </summary>
        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 请求失败返回的子错误码
        /// </summary>
        [JsonProperty("error_sub_code")]
        public string ErrorSubCode { get; set; }

        /// <summary>
        /// 请求失败返回的子错误信息
        /// </summary>
        [JsonProperty("error_sub_msg")]
        public string ErrorSubMsg { get; set; }
    }

    /// <summary>
    /// 映射成功模型
    /// </summary>
    public class DMMapDTO<T> : IBaseMapDTO<T>
    {
        /// <summary>
        /// 平台颁发的每次请求访问的唯一标识
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// 源数据
        /// </summary>
        [JsonProperty("original_body")]
        public string OriginalBody { get; set; }

        /// <summary>
        /// 映射结果
        /// </summary>
        [JsonProperty("qimen_body")]
        public T Body { get; set; }


        /// <summary>
        /// 请求失败返回的错误码
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 请求失败返回的错误信息
        /// </summary>
        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 请求失败返回的子错误码
        /// </summary>
        [JsonProperty("error_sub_code")]
        public string ErrorSubCode { get; set; }

        /// <summary>
        /// 请求失败返回的子错误信息
        /// </summary>
        [JsonProperty("error_sub_msg")]
        public string ErrorSubMsg { get; set; }
    }
}
