using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Hubs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ChannelService : BackgroundService, IChannelService
    {
        private readonly IHubContext<DeviceDataOutHub> _hubContext;
        private readonly ILogger<ChannelService> _logger;
        private int _capacity = 100;
        private Channel<DeviceData> _channel;

        public ChannelService(IHubContext<DeviceDataOutHub> hubContext, ILogger<ChannelService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
            _channel = Channel.CreateBounded<DeviceData>(_capacity);
        }

        public async Task WriteAsync(DeviceData deviceData)
        {
            _logger.LogInformation($"New message from Device {deviceData.Id} at {DateTime.Now}");
            await _channel.Writer.WriteAsync(deviceData);
        }

        public ChannelReader<DeviceData> ClientStarted(CancellationToken ct)
        {
            return _channel.Reader;
        }


        // TODO: MOVE THIS LOGIC TO CLIENT
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await _channel.Reader.WaitToReadAsync(stoppingToken);
                if (_channel.Reader.TryRead(out var deviceData))
                {
                    _logger.LogInformation($"========> To clients from Device {deviceData.Id} at {DateTime.Now}");
                    await _hubContext.Clients.All.SendAsync("NewDeviceData", deviceData, stoppingToken);
                }
            }
        }
    }
}
