using Shared.Models;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IChannelService
    {
        Task WriteAsync(DeviceData deviceData);
    }
}