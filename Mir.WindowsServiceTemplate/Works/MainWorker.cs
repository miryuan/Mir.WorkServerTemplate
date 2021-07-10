using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mir.WindowsServiceTemplate.DependencyInjection.Orm;
using Mir.WindowsServiceTemplate.DependencyInjection.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mir.WindowsServiceTemplate.Works
{
    public class MainWorker : BackgroundService
    {
        private readonly ILogger<MainWorker> _logger;
        private readonly ISqlSugarDbContextFactory _sugar;
        private readonly RedisDbContext _redis;

        public MainWorker(ILogger<MainWorker> logger, ISqlSugarDbContextFactory sugar, RedisDbContext db)
        {
            _logger = logger;
            _sugar = sugar;
            _redis = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var li1 = _sugar.DbContext("Default").Queryable<sys_dept>().ToList();
            //_redis.ToDb(0).Set(Guid.NewGuid().ToString("n"), log, DateTime.Now.AddMinutes(1));

            while (!stoppingToken.IsCancellationRequested)
            {
                string log = string.Format("Worker running at: {0}", DateTimeOffset.Now);

                _logger.LogInformation(log);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
