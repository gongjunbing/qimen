using SqlSugar;

namespace QiMen.DbModel
{
    /// <summary>
    /// 
    /// </summary>

    [SugarTable("qm_user_app_relation")]
    public class QmUserAppRelation
    {
        /// <summary>
        /// 
        /// </summary>
        public QmUserAppRelation()
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
        public System.Boolean? IsDelete { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [SugarColumn(ColumnName ="user_id")]
        public System.String UserId { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        [SugarColumn(ColumnName ="shop_id")]
        public System.String ShopId { get; set; }

        /// <summary>
        /// 奇门应用主键
        /// </summary>
        [SugarColumn(ColumnName ="app_id")]
        public System.Int64 AppId { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        [SugarColumn(ColumnName ="app_key")]
        public System.String AppKey { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        [SugarColumn(ColumnName ="app_secret")]
        public System.String AppSecret { get; set; }

        /// <summary>
        /// 应用沙箱地址
        /// </summary>
        [SugarColumn(ColumnName ="app_sandbox_url")]
        public System.String AppSandboxUrl { get; set; }

        /// <summary>
        /// 应用生产环境地址
        /// </summary>
        [SugarColumn(ColumnName ="app_product_url")]
        public System.String AppProductUrl { get; set; }

        /// <summary>
        /// 自增编码
        /// </summary>
        [SugarColumn(ColumnName ="auto_number",IsIdentity = true)]
        public System.Int64 AutoNumber { get; set; }
    }
}