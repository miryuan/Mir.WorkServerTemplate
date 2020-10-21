using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mir.WorkServer.DependencyInjection;
using Mir.WorkServer.Works;
using System.IO;
using System.Linq;

namespace Mir.WorkServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            return Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(config);//注入配置文件(必须)
                    services.AddSqlSugar();//SqlSugar
                    services.AddRedis();//Redis
                    //默认主任务
                    services.AddHostedService<MainWorker>();//可以在这里添加另外的工作服务
                })
                .ConfigureLogging(builder => builder.AddFile());//日志写文件
        }
    }
}
