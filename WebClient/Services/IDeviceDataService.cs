using System;
using System.Threading.Tasks;

namespace WebClient.Services
{
    

    public interface IDeviceDataService 
    {
        public event EventHandler<DeviceDataEventArgs> DeviceDataEvent;

    }
}