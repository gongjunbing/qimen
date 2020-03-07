using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_app_system_params")]
    public class QmAppSystemParams
    {
        /// <summary>
        /// 
        /// </summary>
        public QmAppSystemParams()
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
        /// 奇门应用主键
        /// </summary>
        [SugarColumn(ColumnName ="app_id")]
        public System.Int64 AppId { get; set; }

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
    }
}