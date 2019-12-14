using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public class DeviceDataService : IDeviceDataService, IHostedService 
    {
        IHubConnectionBuilder _hubConnectionBuilder;
        public DeviceDataService(IHubConnectionBuilder hubConnectionBuilder)
        {
            _hubConnectionBuilder = hubConnectionBuilder;
        }

        public event EventHandler<DeviceDataEventArgs> DeviceDataEvent;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var connection = _hubConnectionBuilder
                .WithUrl("")
                .Build();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
