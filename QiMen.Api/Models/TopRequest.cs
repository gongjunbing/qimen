using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QiMen.Models
{
    /// <summary>
    /// 请求入参
    /// </summary>
    public class TopRequest
    {
        /// <summary>
        /// 应用简称
        /// </summary>
        [Required(ErrorMessage = "app_name不可为空", AllowEmptyStrings = false)]
        [MaxLength(20, ErrorMessage = "app_name超长")]
        [ModelBinder(Name = "app_name")]
        public string AppShortName { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "user_id不可为空")]
        [MaxLength(50, ErrorMessage = "user_id超长")]
        [ModelBinder(Name = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        [Required(ErrorMessage = "shop_id不可为空")]
        [MaxLength(50, ErrorMessage = "shop_id超长")]
        [ModelBinder(Name = "shop_id")]
        public string ShopId { get; set; }

        /// <summary>
        /// Api标识
        /// </summary>
        [Required(ErrorMessage = "method不可为空")]
        [MaxLength(200, ErrorMessage = "method超长")]
        [ModelBinder(Name = "method")]
        public string ApiKey { get; set; }
    }
}
