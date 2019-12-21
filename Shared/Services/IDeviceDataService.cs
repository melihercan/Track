using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services
{
    public interface IDeviceDataService
    {
        public event EventHandler<DeviceDataEventArgs> OnNewDataEvent;

    }
}
