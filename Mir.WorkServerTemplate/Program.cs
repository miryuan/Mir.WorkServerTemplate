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
                    services.AddSingleton(config);//ע�������ļ�(����)
                    services.AddSqlSugar();//SqlSugar
                    services.AddRedis();//Redis
                    //Ĭ��������
                    services.AddHostedService<MainWorker>();//�����������������Ĺ�������
                })
                .ConfigureLogging(builder => builder.AddFile());//��־д�ļ�
        }
    }
}
