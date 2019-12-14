using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Models;
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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var connection = _hubConnectionBuilder
                .WithUrl("https://localhost:44344/DeviceData")
                .WithAutomaticReconnect()
                .AddMessagePackProtocol()
                .Build();
            await connection.StartAsync();

            var cts = new CancellationTokenSource();
            connection.Closed += ex =>
            {
                cts.Cancel();
                return Task.CompletedTask;
            };

            connection.On("NewDeviceData", () =>
            {

            });

            var channel = await connection.StreamAsChannelAsync<DeviceData>("StreamDeviceData", CancellationToken.None);
            while(await channel.WaitToReadAsync() && !cts.IsCancellationRequested)
            {
                while (channel.TryRead(out var deviceData))
                {
                    DeviceDataEvent?.Invoke(this, new DeviceDataEventArgs
                    {
                        DeviceData = deviceData
                    });
                }
            }

        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
