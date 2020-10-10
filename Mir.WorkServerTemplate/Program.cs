using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mir.WorkServer.Extension;
using Mir.WorkServer.Service;
using Mir.WorkServer.Works;
using System;

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
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransientSettingService(SqlSugar.DbType.Sqlite, true, string.Format("Data Source={0};", AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Settings.db"));
                    services.AddHostedService<MainWorker>();//可以在这里添加另外的工作服务
                });
        }
    }
}
