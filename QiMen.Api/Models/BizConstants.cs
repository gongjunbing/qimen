namespace QiMen.Models
{
    /// <summary>
    /// 业务常量
    /// </summary>
    public sealed class BizConstants
    {
        /// <summary>
        /// 不可为空
        /// </summary>
        public const string NOT_NULL = "不可为空";

        /// <summary>
        /// 不正确
        /// </summary>
        public const string NOT_CORRECT = "不正确";

        /// <summary>
        /// 超出上限
        /// </summary>
        public const string OVER_LIMIT = "超出上限";

        /// <summary>
        /// 页码最小值
        /// </summary>
        public const int PAGE_INDEX_MIN = 1;

        /// <summary>
        /// 页码最大值
        /// </summary>
        public const int PAGE_INDEX_MAX = 999999;

        /// <summary>
        /// 整数最大值
        /// </summary>
        public const int INT_MAX = 999999;

        /// <summary>
        /// 页大小最小值
        /// </summary>
        public const int PAGE_SIZE_MIN = 1;

        /// <summary>
        /// 页大小最大值
        /// </summary>
        public const int PAGE_SIZE_MAX = 100;

        /// <summary>
        /// 批量操作最大值
        /// </summary>
        public const int BATHCH_SIZE_MAX = 200;
    }
}
