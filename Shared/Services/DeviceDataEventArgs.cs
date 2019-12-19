using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services
{
    public class DeviceDataEventArgs : EventArgs
    {
        public DeviceData DeviceData { get; set; }
    }
}
