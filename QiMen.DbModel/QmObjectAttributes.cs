using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_object_attributes")]
    public class QmObjectAttributes
    {
        /// <summary>
        /// 
        /// </summary>
        public QmObjectAttributes()
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
        /// 对象Id
        /// </summary>
        [SugarColumn(ColumnName ="object_id")]
        public System.Int64 ObjectId { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [SugarColumn(ColumnName ="attributes_name")]
        public System.String AttributesName { get; set; }

        /// <summary>
        /// 属性描述
        /// </summary>
        [SugarColumn(ColumnName ="attributes_desc")]
        public System.String AttributesDesc { get; set; }

        /// <summary>
        /// 属性KEY
        /// </summary>
        [SugarColumn(ColumnName ="attributes_key")]
        public System.String AttributesKey { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [SugarColumn(ColumnName ="object_type")]
        public System.String ObjectType { get; set; }

        /// <summary>
        /// 关联对象id
        /// </summary>
        [SugarColumn(ColumnName ="relation_id")]
        public System.Int64? RelationId { get; set; }

        /// <summary>
        /// 是否集合
        /// </summary>
        [SugarColumn(ColumnName ="is_list")]
        public System.Boolean IsList { get; set; }
    }
}