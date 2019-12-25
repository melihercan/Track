using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Hubs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ChannelService : BackgroundService, IChannelService
    {
        private IHubContext<DeviceDataOutHub> _hubContext;
        private ILogger<ChannelService> _logger;


        public ChannelService(IHubContext<DeviceDataOutHub> hubContext, ILogger<ChannelService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task WriteAsync(DeviceData deviceData)
        {
            _logger.LogInformation($"New message from Device {deviceData.Id} at {DateTime.Now}");

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }
    }
}
