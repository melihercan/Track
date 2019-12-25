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
        private readonly IChannelService _channelService;
        private readonly ILogger<DeviceDataInHub> _logger;

        public DeviceDataInHub(IChannelService channelService, ILogger<DeviceDataInHub> logger)
        {
            _channelService = channelService;
            _logger = logger;
        }

        public async Task NewMessageAsync(DeviceData deviceData)
        {
////            _logger.LogInformation($"New message from Device {deviceData.Id} at {DateTime.Now}");
            await _channelService.WriteAsync(deviceData);
        }
    }
}
