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

namespace Device
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _numDevices = 5;
        private readonly int _delay = 3000;



        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = new Task[_numDevices];

            for (int i=0; i<_numDevices; i++)
            {
                tasks[i] = DoWorkAsync(i, stoppingToken);
                await Task.Delay(_delay / _numDevices);
            }

            Task.WaitAll(tasks);
        }

        private async Task DoWorkAsync(int id, CancellationToken ct)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/DeviceDataIn")
////                .WithAutomaticReconnect()
                .AddMessagePackProtocol()
                .Build();

            connection.Closed += ex =>
            {
                // Start connection again without awaiting.
                _ = ConnectWithRetryAsync(connection, ct);
                return Task.CompletedTask;
            };

            // Await till connection is made or cancel occured.
            await ConnectWithRetryAsync(connection, ct);

            while (!ct.IsCancellationRequested)
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    await connection.SendAsync("NewMessageAsync", new DeviceData 
                    { 
                        Id = id,
                        GroupId = 0,
                        Timestamp = DateTime.Now,
                        Latitude = 1,
                        Longitude = 2,
                        Altitude = 3,
                        Speed = 4
                    });
                    _logger.LogInformation($"Device {id} NewMessageAsync at {DateTime.Now}");
                }
                await Task.Delay(_delay, ct);
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
