using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_api")]
    public class QmApi
    {
        /// <summary>
        /// 
        /// </summary>
        public QmApi()
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
        /// 原接口key
        /// </summary>
        [SugarColumn(ColumnName ="origin_api_key")]
        public System.String OriginApiKey { get; set; }

        /// <summary>
        /// 奇门接口key
        /// </summary>
        [SugarColumn(ColumnName ="qm_api_key")]
        public System.String QmApiKey { get; set; }

        /// <summary>
        /// 接口环境（沙箱：sandbox；生产：product）
        /// </summary>
        [SugarColumn(ColumnName ="environment")]
        public System.String Environment { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [SugarColumn(ColumnName ="method")]
        public System.String Method { get; set; }

        /// <summary>
        /// 是否上线0否，1是
        /// </summary>
        [SugarColumn(ColumnName ="is_online")]
        public System.Boolean IsOnline { get; set; }

        /// <summary>
        /// 是否需要签名鉴权0否，1是
        /// </summary>
        [SugarColumn(ColumnName ="is_sign")]
        public System.Boolean IsSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName ="object_id")]
        public System.Int64 ObjectId { get; set; }
    }
}