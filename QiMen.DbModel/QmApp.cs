using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 奇门接入应用表
    /// </summary>

    [SugarTable("qm_app")]
    public class QmApp
    {
        /// <summary>
        /// 奇门接入应用表
        /// </summary>
        public QmApp()
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName ="id",IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 自增编码
        /// </summary>
        [SugarColumn(ColumnName ="auto_number",IsIdentity = true)]
        public System.Int64 AutoNumber { get; set; }

        /// <summary>
        /// 应用简称
        /// </summary>
        [SugarColumn(ColumnName ="short_name")]
        public System.String ShortName { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [SugarColumn(ColumnName ="name")]
        public System.String Name { get; set; }

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
    }
}