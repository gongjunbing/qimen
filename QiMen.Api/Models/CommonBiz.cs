using System.ComponentModel.DataAnnotations;

namespace QiMen.Models
{
    /// <summary>
    /// 分页业务
    /// </summary>
    public class PageBiz
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Display(Name = "页码")]
        [Required(ErrorMessage = "{0}" + BizConstants.NOT_NULL)]
        [Range(BizConstants.PAGE_INDEX_MIN, BizConstants.PAGE_INDEX_MAX, ErrorMessage = "{0}" + BizConstants.NOT_CORRECT)]
        public int CurrentPage { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        [Display(Name = "页大小")]
        [Required(ErrorMessage = "{0}" + BizConstants.NOT_NULL)]
        [Range(BizConstants.PAGE_SIZE_MIN, BizConstants.PAGE_SIZE_MAX, ErrorMessage = "{0}" + BizConstants.NOT_CORRECT)]
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 主键id
    /// </summary>
    public class IdBiz
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Required(ErrorMessage = "id" + BizConstants.NOT_NULL)]
        public System.Int64 Id { get; set; }
    }

    /// <summary>
    /// 批量操作主键id
    /// </summary>
    public class BatchIdBiz
    {
        /// <summary>
        /// 主键id数组
        /// </summary>
        [MinLength(1, ErrorMessage = "id" + BizConstants.NOT_NULL)]
        [MaxLength(BizConstants.BATHCH_SIZE_MAX, ErrorMessage = "id" + BizConstants.OVER_LIMIT)]
        public System.Int64[] Ids { get; set; }
    }

    /// <summary>
    /// 上传请求
    /// </summary>
    public class UploadBiz
    {
        /// <summary>
        /// 文件
        /// </summary>
        [Required(ErrorMessage = "上传文件" + BizConstants.NOT_NULL)]
        public Microsoft.AspNetCore.Http.IFormCollection UploadFile { get; set; }
    }

}
