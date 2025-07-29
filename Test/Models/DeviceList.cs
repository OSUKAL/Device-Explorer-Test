using Test.Exceptions;
using Test.ViewModel;

namespace Test.Model;

public class DeviceList
{
    private readonly List<Device> _devices;

    public DeviceList()
    {
        _devices = [];
    }

    public void AddDevice(Device device)
    {
        foreach (var existingDevice in _devices)
        {
            if (existingDevice.Conflicts(device))
            {
                throw new DeviceConflictException(existingDevice, device);
            }
        }
        
        _devices.Add(device);
    }
}