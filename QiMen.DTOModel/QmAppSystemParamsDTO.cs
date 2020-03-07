namespace QiMen.DTOModel
{
    /// <summary>
    /// 应用系统级公共入参
    /// </summary>
    public class QmAppSystemParamsDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
