using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mir.WorkServer.Entity;
using Mir.WorkServer.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mir.WorkServer.Works
{
    public class MainWorker : BackgroundService
    {
        private readonly ILogger<MainWorker> _logger;
        private readonly SettingService _db;

        public MainWorker(ILogger<MainWorker> logger, SettingService setting)
        {
            _logger = logger;
            _db = setting;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var settings = _db.Client.Queryable<Settings>().ToList();
            


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
