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
                    services.AddSingleton(config);//ע�������ļ�(����)
                    services.AddSqlSugar();

                    ////���ÿ����ע��
                    //string dbPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Settings.db";
                    //services.AddTransientSettingService(SqlSugar.DbType.Sqlite, true, string.Format("Data Source={0};", dbPath));

                    ////��ȡ�����ļ�,����Ĭ�����ݿ����ע��
                    //var connections = config.GetSection("Connection").Get<IEnumerable<Model.Connection>>().ToList();
                    //var defaultConnection = connections.Find(c => c.Name == "Default");
                    //if (defaultConnection != null)
                    //    services.AddTransientDefaultSqlSugarService(defaultConnection.DbType, defaultConnection.IsAutoCloseConnection, defaultConnection.ConnectionString);

                    //Ĭ��������
                    services.AddHostedService<MainWorker>();//�����������������Ĺ�������
                });
        }
    }
}
