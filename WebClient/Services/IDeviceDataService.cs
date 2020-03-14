using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebClient.Services
{

    public interface IDeviceDataService
    {
        public event EventHandler<DeviceDataEventArgs> OnDataEvent;

        public Task StartAsync(CancellationToken ct);
    }
}