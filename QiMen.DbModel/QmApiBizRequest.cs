using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_api_biz_request")]
    public class QmApiBizRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public QmApiBizRequest()
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName ="id",IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName ="create_time")]
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName ="update_time")]
        public System.DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除时间（假删时设置）
        /// </summary>
        [SugarColumn(ColumnName ="delete_time")]
        public System.DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 是否删除0否，1是
        /// </summary>
        [SugarColumn(ColumnName ="is_delete")]
        public System.Boolean IsDelete { get; set; }

        /// <summary>
        /// 自增编码
        /// </summary>
        [SugarColumn(ColumnName ="auto_number",IsIdentity = true)]
        public System.Int64 AutoNumber { get; set; }

        /// <summary>
        /// 奇门接口主键
        /// </summary>
        [SugarColumn(ColumnName ="api_id")]
        public System.Int64 ApiId { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        [SugarColumn(ColumnName ="name")]
        public System.String Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [SugarColumn(ColumnName ="type")]
        public System.String Type { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [SugarColumn(ColumnName ="is_required")]
        public System.Boolean IsRequired { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [SugarColumn(ColumnName ="default_value")]
        public System.String DefaultValue { get; set; }

        /// <summary>
        /// 示例值
        /// </summary>
        [SugarColumn(ColumnName ="example")]
        public System.String Example { get; set; }

        /// <summary>
        /// 说明描述
        /// </summary>
        [SugarColumn(ColumnName ="desc")]
        public System.String Desc { get; set; }

        /// <summary>
        /// 数值型最小值
        /// </summary>
        [SugarColumn(ColumnName ="min_value")]
        public System.Int32 MinValue { get; set; }

        /// <summary>
        /// 数值型最大值
        /// </summary>
        [SugarColumn(ColumnName ="max_value")]
        public System.Int32 MaxValue { get; set; }

        /// <summary>
        /// 字符型最小长度
        /// </summary>
        [SugarColumn(ColumnName ="min_length")]
        public System.Int32 MinLength { get; set; }

        /// <summary>
        /// 字符型最大长度
        /// </summary>
        [SugarColumn(ColumnName ="max_length")]
        public System.Int32 MaxLength { get; set; }
    }
}