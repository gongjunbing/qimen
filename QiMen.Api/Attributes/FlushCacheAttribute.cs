using System;

namespace QiMen.Api.Attributes
{
    /// <summary>
    /// 释放缓存
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FlushCacheAttribute : Attribute
    {
        /// <summary>
        /// 是否释放
        /// </summary>
        public bool IsFlush { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isFlush"></param>
        public FlushCacheAttribute(bool isFlush = true)
        {
            this.IsFlush = isFlush;
        }
    }
}
