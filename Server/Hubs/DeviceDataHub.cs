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
    public class DeviceDataHub : Hub<IDeviceDataClient>
    {

        //public ChannelReader<DeviceData> StreamDeviceData()
        //{
        //    return null;
        //}

        public async Task SendMessage()
        {
            await Clients.All.ReceiveDeviceData(new DeviceData 
            { 
                Id = 1,
                GroupId = 1,
            });
        }

    }
}
