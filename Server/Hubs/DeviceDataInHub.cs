using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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
        ILogger<DeviceDataInHub> _logger;

        public DeviceDataInHub(ILogger<DeviceDataInHub> logger)
        {
            _logger = logger;
        }

        public async Task NewMessageAsync(DeviceData deviceData)
        {
            _logger.LogInformation($"New message from Device {deviceData.Id} at {DateTime.Now}");
        }

///        private IHubContext<DeviceDataOutHub> _outHubContext;

////        public DeviceDataInHub(IHubContext<DeviceDataOutHub> outHubContext)
////        {
////            _outHubContext = outHubContext;
////        }

///        public ChannelReader<DeviceData> StreamDeviceData()
   ///     {
      //      return null;
        //}

        //public async Task SendMessage()
        //{
            ////await _outHubContext.Clients.All.SendAsync("Xxx", "fix me");
            //await _outHubContext.Clients.All.NewDeviceData(new DeviceData 
            //{ 
            //    Id = 1,
            //    GroupId = 1,
            //});
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
