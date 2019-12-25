using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Services;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class DeviceDataInHub : Hub
    {
        private IChannelService _channel;
        private ILogger<DeviceDataInHub> _logger;

        public DeviceDataInHub(IChannelService channel, ILogger<DeviceDataInHub> logger)
        {
            _channel = channel;
            _logger = logger;
        }

        public async Task NewMessageAsync(DeviceData deviceData)
        {
////            _logger.LogInformation($"New message from Device {deviceData.Id} at {DateTime.Now}");
            await _channel.WriteAsync(deviceData);
        }
    }
}
