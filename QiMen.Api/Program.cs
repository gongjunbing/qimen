using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace QiMen.Api
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主程序
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 创建WebHost宿主机
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
