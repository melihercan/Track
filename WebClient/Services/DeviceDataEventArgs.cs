using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public class DeviceDataEventArgs : EventArgs
    {
        public DeviceData DeviceData { get; set; }

    }
}
