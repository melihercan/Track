using Microsoft.AspNetCore.SignalR;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class DeviceDataInHub : Hub<IDeviceDataClient>
    {
        private IHubContext<DeviceDataOutHub> _outHubContext;

        public DeviceDataInHub(IHubContext<DeviceDataOutHub> outHubContext)
        {
            _outHubContext = outHubContext;
        }

        public ChannelReader<DeviceData> StreamDeviceData()
        {
            return null;
        }

        public async Task SendMessage()
        {
            await _outHubContext.Clients.All.SendAsync("Xxx", "fix me");
            //await _outHubContext.Clients.All.NewDeviceData(new DeviceData 
            //{ 
            //    Id = 1,
            //    GroupId = 1,
            //});
        }

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
