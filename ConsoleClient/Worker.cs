using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models;

namespace ConsoleClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/DeviceDataOut")
                .AddMessagePackProtocol()
                .Build();

            connection.Closed += ex =>
            {
                // Start connection again without awaiting.
                _ = ConnectWithRetryAsync(connection, stoppingToken);
                return Task.CompletedTask;
            };

            // Await till connection is made or cancel occured.
            await ConnectWithRetryAsync(connection, stoppingToken);

            var channel = await connection.StreamAsChannelAsync<DeviceData>("StreamDeviceData", CancellationToken.None);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    await channel.WaitToReadAsync(stoppingToken);
                    if (channel.TryRead(out var deviceData))
                    {
                        _logger.LogInformation($"========> To clients from Device {deviceData.Id} at {DateTime.Now}");
                    }
                }
            }
        }


        private async Task<bool> ConnectWithRetryAsync(HubConnection connection, CancellationToken ct)
        {
            // Keep trying until we can start or the token is canceled.
            while (true)
            {
                try
                {
                    await connection.StartAsync(ct);
                    Debug.Assert(connection.State == HubConnectionState.Connected);
                    _logger.LogInformation($"Connected to hub with ConnectionId: {connection.ConnectionId}");
                    return true;
                }
                catch when (ct.IsCancellationRequested)
                {
                    return false;
                }
                catch
                {
                    // Failed to connect, trying again in 5000 ms.
                    Debug.Assert(connection.State == HubConnectionState.Disconnected);
                    _logger.LogInformation($"Will try to connect to hub again after 5s...");
                    await Task.Delay(5000);
                }
            }
        }
    }

}

