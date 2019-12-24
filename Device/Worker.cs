using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Device
{
    public class Worker : BackgroundService
    {
        private readonly int _numDevices = 5;
        private readonly int _delay = 1000;
        private readonly ILogger<Worker> _logger;



        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = new Task[_numDevices];

            for (int i=0; i<_numDevices; i++)
            {
                tasks[i] = DoWorkAsync(i, stoppingToken);
                await Task.Delay(_delay / _numDevices);
            }

            Task.WaitAll(tasks);
        }

        private async Task DoWorkAsync(int id, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _logger.LogInformation($"Device {id} running at {DateTimeOffset.Now}");
                await Task.Delay(_delay, ct);
            }
        }
    }
}
