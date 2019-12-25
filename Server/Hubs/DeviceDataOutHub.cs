using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Services;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class DeviceDataOutHub : Hub
    {
        private readonly IChannelService _channelService;
        private readonly ILogger<DeviceDataInHub> _logger;


        public DeviceDataOutHub(IChannelService channelService, ILogger<DeviceDataInHub> logger)
        {
            _channelService = channelService;
            _logger = logger;
        }


        public ChannelReader<DeviceData> StreamDeviceData(CancellationToken ct)
        {
            return _channelService.ClientStarted(ct);
        }


        //public async Task SendMessage()
        //{
        //    await Clients.All.NewDeviceData(new DeviceData 
        //    { 
        //        Id = 1,
        //        GroupId = 1,
        //    });
        //}

        //public DeviceData StreamDeviceData()
        //{
        //    return new DeviceData
        //    {
        //        Id = 1,
        //        GroupId = 1,
        //    };
        //}

    }
}
