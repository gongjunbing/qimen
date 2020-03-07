using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_object")]
    public class QmObject
    {
        /// <summary>
        /// 
        /// </summary>
        public QmObject()
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
        /// 对象名称
        /// </summary>
        [SugarColumn(ColumnName ="object_name")]
        public System.String ObjectName { get; set; }

        /// <summary>
        /// 对象描述
        /// </summary>
        [SugarColumn(ColumnName ="object_desc")]
        public System.String ObjectDesc { get; set; }

        /// <summary>
        /// 对象KEY
        /// </summary>
        [SugarColumn(ColumnName ="object_key")]
        public System.String ObjectKey { get; set; }
    }
}