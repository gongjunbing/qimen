using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QiMen.Models
{
    /// <summary>
    /// 应用列表请求参数
    /// </summary>
    public class GetAppListRequest: PageBiz
    {
        /// <summary>
        /// 平台简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 平台简称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 应用接口列表请求参数
    /// </summary>
    public class GetApiListRequest : PageBiz
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 奇门接口key
        /// </summary>
        public string QmApiKey { get; set; }

        /// <summary>
        /// 原接口key
        /// </summary>
        public string OriginApiKey { get; set; }
    }

    /// <summary>
    /// 对象列表请求参数
    /// </summary>
    public class GetObjectListRequest : PageBiz
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// 对象KEY
        /// </summary>
        public string ObjectKey { get; set; }
    }

    /// <summary>
    /// 应用详细请求参数
    /// </summary>
    public class GetAppDetailRequest
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        [Required(ErrorMessage = "id" + BizConstants.NOT_NULL)]
        public long AppId { get; set; }
    }

    /// <summary>
    /// 新增应用请求参数
    /// </summary>
    public class AddAppRequest
    {
        /// <summary>
        /// 应用简称
        /// </summary>
        [Required(ErrorMessage = "应用简称" + BizConstants.NOT_NULL)]
        public string ShortName { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [Required(ErrorMessage = "应用名称" + BizConstants.NOT_NULL)]
        public string Name { get; set; }

        /// <summary>
        /// 应用公共请求参数
        /// </summary>
        public List<AddSystemParam> SystemParamList { get; set; }
    }

    /// <summary>
    /// 新增应用请求参数
    /// </summary>
    public class AddSystemParamRequest
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        [Required(ErrorMessage = "应用Id" + BizConstants.NOT_NULL)]
        public long AppId { get; set; }

        /// <summary>
        /// 应用公共请求参数
        /// </summary>
        public List<AddSystemParam> SystemParamList { get; set; }
    }

    /// <summary>
    /// 新增应用公共请求参数
    /// </summary>
    public class AddSystemParam
    {

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称" + BizConstants.NOT_NULL)]
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Required(ErrorMessage = "参数类型" + BizConstants.NOT_NULL)]
        public string Type { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 示例值
        /// </summary>
        public string Example { get; set; }
    }

    /// <summary>
    /// 新增用户请求参数
    /// </summary>
    public class AddUserRequest
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        [Required(ErrorMessage = "应用Id" + BizConstants.NOT_NULL)]
        public long AppId { get; set; }

        /// <summary>
        /// 新增用户
        /// </summary>
        public List<AddUser> UserList { get; set; }
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    public class AddUser
    {

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id" + BizConstants.NOT_NULL)]
        public string UserId { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        [Required(ErrorMessage = "应用key" + BizConstants.NOT_NULL)]
        public string AppKey { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 应用沙箱地址
        /// </summary>
        public string AppSandboxUrl { get; set; }

        /// <summary>
        /// 应用生产环境地址
        /// </summary>
        public string AppProductUrl { get; set; }
    }

    /// <summary>
    /// 删除应用请求参数
    /// </summary>
    public class DeleteAppRequest
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        [Required(ErrorMessage = "id" + BizConstants.NOT_NULL)]
        public long AppId { get; set; }
    }

    /// <summary>
    /// 删除应用公共请求参数
    /// </summary>
    public class DeleteSystemParamRequest
    {
        /// <summary>
        /// 公共请求参数Id
        /// </summary>
        public List<long> SystemParamId { get; set; }
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    public class DeleteUserRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public List<long> UserId { get; set; }
    }

    /// <summary>
    /// 新增接口
    /// </summary>
    public class AddApiRequest
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        [Required(ErrorMessage = "应用Id" + BizConstants.NOT_NULL)]
        public long AppId { get; set; }

        /// <summary>
        /// 原接口key
        /// </summary>
        [Required(ErrorMessage = "原接口key" + BizConstants.NOT_NULL)]
        public string OriginApiKey { get; set; }

        /// <summary>
        /// 奇门接口key
        /// </summary>
        [Required(ErrorMessage = "奇门接口key" + BizConstants.NOT_NULL)]
        public string QmApiKey { get; set; }

        /// <summary>
        /// 接口环境（沙箱：sandbox；生产：product）
        /// </summary>
        [Required(ErrorMessage = "接口环境" + BizConstants.NOT_NULL)]
        public string Environment { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [Required(ErrorMessage = "请求方式" + BizConstants.NOT_NULL)]
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
        /// 映射对象id
        /// </summary>
        [Required(ErrorMessage = "映射对象id" + BizConstants.NOT_NULL)]
        public long ObjectId { get; set; }

        /// <summary>
        /// 接口请求参数集合
        /// </summary>
        public List<ApiBizRequest> ApiBizRequestList { get; set; }

        /// <summary>
        /// 接口返回参数集合
        /// </summary>
        public List<ApiRespone> ApiResponeList { get; set; }
    }

    /// <summary>
    /// 删除接口
    /// </summary>
    public class DeleteApiRequest
    {
        /// <summary>
        /// 接口Id
        /// </summary>
        [Required(ErrorMessage = "接口Id" + BizConstants.NOT_NULL)]
        public long ApiId { get; set; }
    }

    /// <summary>
    /// 获取接口详情
    /// </summary>
    public class GetApiDetailRequest
    {
        /// <summary>
        /// 接口Id
        /// </summary>
        [Required(ErrorMessage = "接口Id" + BizConstants.NOT_NULL)]
        public long ApiId { get; set; }
    }

    /// <summary>
    /// 新增接口请求参数
    /// </summary>
    public class AddApiBizRequest
    {
        /// <summary>
        /// 接口Id
        /// </summary>
        [Required(ErrorMessage = "接口Id" + BizConstants.NOT_NULL)]
        public long ApiId { get; set; }

        /// <summary>
        /// 接口请求参数集合
        /// </summary>
        public List<ApiBizRequest> ApiBizRequestList { get; set; }
    }

    /// <summary>
    /// 删除接口请求参数
    /// </summary>
    public class DeleteApiBizRequest
    {
        /// <summary>
        /// 请求参数Id
        /// </summary>
        public List<long> RequestId { get; set; }
    }

    /// <summary>
    /// 新增接口返回参数
    /// </summary>
    public class AddApiResponeRequest
    {
        /// <summary>
        /// 接口Id
        /// </summary>
        [Required(ErrorMessage = "接口Id" + BizConstants.NOT_NULL)]
        public long ApiId { get; set; }

        /// <summary>
        /// 接口请求参数集合
        /// </summary>
        public List<ApiRespone> ApiResponeList { get; set; }
    }

    /// <summary>
    /// 删除接口返回参数
    /// </summary>
    public class DeleteApiResponeRequest
    {
        /// <summary>
        /// 返回参数Id
        /// </summary>
        public List<long> ResponeId { get; set; }
    }

    /// <summary>
    /// 接口请求参数
    /// </summary>
    public class ApiBizRequest
    {

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称" + BizConstants.NOT_NULL)]
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Required(ErrorMessage = "参数类型" + BizConstants.NOT_NULL)]
        public string Type { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 示例值
        /// </summary>
        public string Example { get; set; }

        /// <summary>
        /// 说明描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 数值型最小值
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 数值型最大值
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 字符型最小长度
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// 字符型最大长度
        /// </summary>
        public int MaxLength { get; set; }
    }

    /// <summary>
    /// 接口返回参数
    /// </summary>
    public class ApiRespone
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称" + BizConstants.NOT_NULL)]
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Required(ErrorMessage = "参数类型" + BizConstants.NOT_NULL)]
        public string Type { get; set; }

        /// <summary>
        /// 说明描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 对应对象Id
        /// </summary>
        [Required(ErrorMessage = "对应对象Id" + BizConstants.NOT_NULL)]
        public long ObjectId { get; set; }

        /// <summary>
        /// 对应属性KEY
        /// </summary>
        [Required(ErrorMessage = "对应属性KEY" + BizConstants.NOT_NULL)]
        public string AttributeKey { get; set; }
    }

    /// <summary>
    /// 新增对象
    /// </summary>
    public class AddObjectRequest
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        [Required(ErrorMessage = "对象名称" + BizConstants.NOT_NULL)]
        public string ObjectName { get; set; }

        /// <summary>
        /// 对象描述
        /// </summary>
        public string ObjectDesc { get; set; }

        /// <summary>
        /// 对象KEY
        /// </summary>
        [Required(ErrorMessage = "对象KEY" + BizConstants.NOT_NULL)]
        public string ObjectKey { get; set; }

        /// <summary>
        /// 接口请求参数集合
        /// </summary>
        public List<Attributes> AttributesList { get; set; }
    }

    /// <summary>
    /// 获取对象详情
    /// </summary>
    public class GetObjectDetailRequest
    {
        /// <summary>
        /// 对象Id
        /// </summary>
        [Required(ErrorMessage = "对象Id" + BizConstants.NOT_NULL)]
        public long ObjectId { get; set; }
    }

    /// <summary>
    /// 删除对象
    /// </summary>
    public class DeleteObjectRequest
    {
        /// <summary>
        /// 对象Id
        /// </summary>
        [Required(ErrorMessage = "对象Id" + BizConstants.NOT_NULL)]
        public long ObjectId { get; set; }
    }

    /// <summary>
    /// 对象属性
    /// </summary>
    public class Attributes
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        [Required(ErrorMessage = "属性名称" + BizConstants.NOT_NULL)]
        public string AttributesName { get; set; }

        /// <summary>
        /// 属性KEY
        /// </summary>
        [Required(ErrorMessage = "属性KEY" + BizConstants.NOT_NULL)]
        public string AttributesKey { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [Required(ErrorMessage = "属性类型" + BizConstants.NOT_NULL)]
        public string ObjectType { get; set; }

        /// <summary>
        /// 属性描述
        /// </summary>
        public string AttributesDesc { get; set; }

        /// <summary>
        /// 是否集合
        /// </summary>
        public bool IsList { get; set; }

        /// <summary>
        /// 关联对象id
        /// </summary>
        public long? RelationId { get; set; }
    }

    /// <summary>
    /// 新增属性
    /// </summary>
    public class AddAttributeRequest
    {
        /// <summary>
        /// 对象Id
        /// </summary>
        [Required(ErrorMessage = "对象Id" + BizConstants.NOT_NULL)]
        public long ObjectId { get; set; }

        /// <summary>
        /// 属性集合
        /// </summary>
        public List<Attributes> AttributesList { get; set; }
    }

    /// <summary>
    /// 删除属性
    /// </summary>
    public class DeleteAttributeRequest
    {
        /// <summary>
        /// 属性Id
        /// </summary>
        public List<long> AttributesId { get; set; }
    }
}
