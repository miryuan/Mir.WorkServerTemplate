using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mir.WorkServer.Entity;
using Mir.WorkServer.Extension.Orm;
using Mir.WorkServer.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mir.WorkServer.Works
{
    public class MainWorker : BackgroundService
    {
        private readonly ILogger<MainWorker> _logger;
        private readonly ISqlSugarDbContextFactory _sugar;

        public MainWorker(ILogger<MainWorker> logger, ISqlSugarDbContextFactory sugar)
        {
            _logger = logger;
            _sugar = sugar;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var li1 = _sugar.DbContext("Default").Queryable<sys_dept>().ToList();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
