using CommonUtil;
using Newtonsoft.Json.Linq;
using QiMen.DbModel;
using QiMen.IService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QiMen.Service
{
    /// <summary>
    /// 奇门对象服务
    /// </summary>
    public class QmObjectService : DbSet<QmObject>, IQmObjectService
    {
        /// <summary>
        /// json映射
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objectModel"></param>
        /// <param name="apiModel"></param>
        /// <param name="attributesList"></param>
        /// <param name="responeList"></param>
        /// <returns></returns>
        public JObject GetMappingModel(JToken json,QmObject objectModel,QmApi apiModel,List<QmObjectAttributes> attributesList=null, List<QmRespone> responeList = null)
        {
            var result = new JObject();

            if (attributesList==null)
            {
                attributesList=base.Db.Queryable<QmObjectAttributes>().Where(a => a.IsDelete == false && a.ObjectId == objectModel.Id).ToList();
            }

            if (responeList==null)
            {
                responeList = base.Db.Queryable<QmRespone>().Where(a => a.IsDelete == false && a.ApiId == apiModel.Id).ToList();
            }

            foreach (var item in attributesList)
            {
                var respone = responeList.FirstOrDefault(a=>a.AttributeKey==item.AttributesKey&&a.ObjectId== objectModel.Id);
                if (respone==null)
                {
                    continue;
                }

                JToken itemJson = json[respone.Name];
                switch (item.ObjectType)
                {
                    case "string":
                        {
                            result.Add(new JProperty(item.AttributesKey, itemJson));
                            break;
                        }
                    case "int":
                        {
                            result.Add(new JProperty(item.AttributesKey, itemJson));
                            break;
                        }
                    case "dynamic":
                        {
                            var childModel = base.GetById(Convert.ToInt64(item.RelationId));
                            var childattributes= base.Db.Queryable<QmObjectAttributes>().Where(a => a.IsDelete == false && a.ObjectId == childModel.Id).ToList();
                            if (item.IsList)
                            {
                                if (itemJson!=null)
                                {
                                    JArray childJson = (JArray)itemJson;
                                    JArray resultJArray = new JArray();
                                    foreach (var childItem in childJson)
                                    {
                                        resultJArray.Add(GetMappingModel(childItem, childModel, apiModel, childattributes, responeList));
                                    }

                                    result.Add(new JProperty(item.AttributesKey, resultJArray));
                                }

                            }
                            else
                            {
                                result.Add(new JProperty(item.AttributesKey, GetMappingModel(itemJson, childModel,apiModel,childattributes,responeList)));
                            }
                            break;
                        }
                    case "bool":
                        {
                            result.Add(new JProperty(item.AttributesKey, itemJson));
                            break;
                        }
                    case "decimal":
                        {
                            result.Add(new JProperty(item.AttributesKey, itemJson));
                            break;
                        }
                }
            }

            return result;
        }
    }
}
