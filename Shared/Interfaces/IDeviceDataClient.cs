﻿using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IDeviceDataClient
    {
        Task NewDeviceData(DeviceData deviceData);
    }
}