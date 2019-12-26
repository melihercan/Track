using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Hubs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ChannelService : BackgroundService, IChannelService
    {
        private readonly ILogger<ChannelService> _logger;
        private int _capacity = 100;
        private readonly Subject<DeviceData> _subject = new Subject<DeviceData>();

        public ChannelService(/*IHubContext<DeviceDataOutHub> hubContext,*/ ILogger<ChannelService> logger)
        {
            _logger = logger;
        }

        public void Write(DeviceData deviceData)
        {
            _logger.LogInformation($"New message from Device {deviceData.Id} at {DateTime.Now}");
            _subject.OnNext(deviceData);
        }

        public ChannelReader<DeviceData> ClientStarted(CancellationToken ct)
        {
            var channel = Channel.CreateBounded<DeviceData>(_capacity);
            var disposable = _subject.Subscribe(
                value => channel.Writer.TryWrite(value),
                error => channel.Writer.TryComplete(error),
                () => channel.Writer.TryComplete());

            // Complete the subscription on the reader completing.
            channel.Reader.Completion.ContinueWith(task => disposable.Dispose());

            return channel.Reader;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //// THIS IS FOR INTERNAL USAGE OF DEVICE DATA SUCH AS STORING TO DBASE

            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                //await _channel.Reader.WaitToReadAsync(stoppingToken);
                //if (_channel.Reader.TryRead(out var deviceData))
                //{
                //    _logger.LogInformation($"--------> To service from Device {deviceData.Id} at {DateTime.Now}");
                //    ////await _hubContext.Clients.All.SendAsync("NewDeviceData", deviceData, stoppingToken);
                //}
            }
        }
    }
}
