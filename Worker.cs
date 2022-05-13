using System;
using System.Threading;
using System.Threading.Tasks;
using AtmDynamicTerminalListWorker.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AtmDynamicTerminalListWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRpaDataService _rpaDataService;
        private readonly int _intervalInHours;

        public Worker(ILogger<Worker> logger, IRpaDataService rpaDataService, IConfiguration configuration)
        {
            _logger = logger;
            _rpaDataService = rpaDataService;

            var successfullyParseIntervalHours = int.TryParse(configuration["IntervalInHours"], out _intervalInHours);

            if (!successfullyParseIntervalHours)
            {
                _intervalInHours = 6;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);

                await _rpaDataService.UpdateAtmLists();

                await Task.Delay(_intervalInHours * 3600000, stoppingToken);
            }
        }
    }
}