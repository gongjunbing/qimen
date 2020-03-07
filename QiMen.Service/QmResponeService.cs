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
    /// 奇门接口返回参数服务
    /// </summary>
    public class QmResponeService : DbSet<QmRespone>, IQmResponeService
    {
    }
}
