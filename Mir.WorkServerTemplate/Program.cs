using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mir.WorkServer.DependencyInjection;
using Mir.WorkServer.Extension;
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
                    services.AddSqlSugar();

                    ////配置库服务注入
                    //string dbPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Settings.db";
                    //services.AddTransientSettingService(SqlSugar.DbType.Sqlite, true, string.Format("Data Source={0};", dbPath));

                    ////读取配置文件,并将默认数据库服务注入
                    //var connections = config.GetSection("Connection").Get<IEnumerable<Model.Connection>>().ToList();
                    //var defaultConnection = connections.Find(c => c.Name == "Default");
                    //if (defaultConnection != null)
                    //    services.AddTransientDefaultSqlSugarService(defaultConnection.DbType, defaultConnection.IsAutoCloseConnection, defaultConnection.ConnectionString);

                    //默认主任务
                    services.AddHostedService<MainWorker>();//可以在这里添加另外的工作服务
                });
        }
    }
}
