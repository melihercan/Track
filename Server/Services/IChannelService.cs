﻿using Shared.Models;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IChannelService
    {
        ///Channel<DeviceData> Channel { get; }

        void Write(DeviceData deviceData);

        ChannelReader<DeviceData> ClientStarted(CancellationToken ct);
    }
}