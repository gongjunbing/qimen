using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_respone")]
    public class QmRespone
    {
        /// <summary>
        /// 
        /// </summary>
        public QmRespone()
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
        /// 接口Id
        /// </summary>
        [SugarColumn(ColumnName ="api_id")]
        public System.Int64 ApiId { get; set; }

        /// <summary>
        /// 自增Id
        /// </summary>
        [SugarColumn(ColumnName ="auto_number",IsIdentity = true)]
        public System.Int64 AutoNumber { get; set; }

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
        /// 参数描述
        /// </summary>
        [SugarColumn(ColumnName ="desc")]
        public System.String Desc { get; set; }

        /// <summary>
        /// 对应属性KEY
        /// </summary>
        [SugarColumn(ColumnName ="attribute_key")]
        public System.String AttributeKey { get; set; }

        /// <summary>
        /// 对象Id
        /// </summary>
        [SugarColumn(ColumnName ="object_id")]
        public System.Int64? ObjectId { get; set; }
    }
}