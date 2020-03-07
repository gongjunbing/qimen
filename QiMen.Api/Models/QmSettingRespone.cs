using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using QiMen.DbModel;

namespace QiMen.Models
{
    /// <summary>
    /// 应用列表返回参数
    /// </summary>
    public class GetAppListRespone
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 平台简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 平台简称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 应用接口列表返回参数
    /// </summary>
    public class GetApiListRespone
    {
        /// <summary>
        /// 接口ID
        /// </summary>
        public long ApiId { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 原接口key
        /// </summary>
        public string OriginApiKey { get; set; }

        /// <summary>
        /// 奇门接口key
        /// </summary>
        public string QmApiKey { get; set; }

        /// <summary>
        /// 接口环境（沙箱：sandbox；生产：product）
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 是否上线0否，1是
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 是否需要签名鉴权0否，1是
        /// </summary>
        public bool IsSign { get; set; }

        /// <summary>
        /// 对象Id
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 对象列表返回参数
    /// </summary>
    public class GetObjectListRespone
    {
        /// <summary>
        /// 对象ID
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// 对象描述
        /// </summary>
        public string ObjectDesc { get; set; }

        /// <summary>
        /// 对象key
        /// </summary>
        public string ObjectKey { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 应用详细
    /// </summary>
    public class GetAppDetailRespone: GetAppListRespone
    {
        /// <summary>
        /// 公共请求参数集合
        /// </summary>
        public List<QmAppSystemParams> SystemParamList { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public List<QmUserAppRelation> UserList { get; set; }

        /// <summary>
        /// 接口集合
        /// </summary>
        public List<QmApi> ApiList { get; set; }
    }

    /// <summary>
    /// 接口详细
    /// </summary>
    public class GetApiDetailRespone : GetApiListRespone
    {
        /// <summary>
        /// 返回参数集合
        /// </summary>
        public List<QmRespone> ResponeList { get; set; }

        /// <summary>
        /// 请求参数集合
        /// </summary>
        public List<QmApiBizRequest> RequestList { get; set; }
    }

    /// <summary>
    /// 对象详细
    /// </summary>
    public class GetObjectDetailRespone : GetObjectListRespone
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        public List<QmObjectAttributes> AttributesList { get; set; }

    }
}
