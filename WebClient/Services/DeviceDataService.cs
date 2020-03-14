﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public class DeviceDataService : IDeviceDataService
    {
        readonly IHubConnectionBuilder _hubConnectionBuilder;
        readonly ILogger<DeviceDataService> _logger;
  
        public DeviceDataService(IHubConnectionBuilder hubConnectionBuilder, ILogger<DeviceDataService> logger)
        {
            _hubConnectionBuilder = hubConnectionBuilder;
            _logger = logger;
            //Task.Run(async () => 
            //{
            //    await StartAsync(new CancellationTokenSource().Token);
            //});
        }

        public event EventHandler<DeviceDataEventArgs> OnDataEvent;

        public async Task StartAsync(CancellationToken ct)
        {
            _logger.LogInformation("=============================EnBuyukFenerbahce2");


            try
            {

                var connection = _hubConnectionBuilder
                    .WithUrl("https://localhost:5001/DeviceData")
                    .WithAutomaticReconnect()
                    .AddMessagePackProtocol()
                    .Build();
                _logger.LogInformation("After _hubConnectionBuilder");
                await connection.StartAsync();
                _logger.LogInformation("After StartAsync");

                var cts = new CancellationTokenSource();

                connection.Closed += ex =>
                {
                    cts.Cancel();
                    return Task.CompletedTask;
                };

                connection.On<DeviceData>("NewDeviceData", deviceData =>
                {
                    OnDataEvent?.Invoke(this, new DeviceDataEventArgs
                    {
                        DeviceData = deviceData
                    });
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"E X C E P T I O N : {ex.Message}");
            }

            //var channel = await connection.StreamAsChannelAsync<DeviceData>("StreamDeviceData", CancellationToken.None);     
            //while(await channel.WaitToReadAsync() && !cts.IsCancellationRequested)
            //{
            //    while (channel.TryRead(out var deviceData))
            //    {
            //        DeviceDataEvent?.Invoke(this, new DeviceDataEventArgs
            //        {
            //            DeviceData = deviceData
            //        });
            //    }
            //}

        }
    }
}
