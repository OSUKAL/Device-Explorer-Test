using Test.Models;

namespace Test.Exceptions;

/// <summary>
/// Исключение возникающее при попытке дублирования в списке устройств существующего серийного номера
/// </summary>
public class DeviceConflictException : Exception
{
    public Device ExistingDevice { get; }
    public Device SuggestedDevice { get; }

    public DeviceConflictException(string message) : base(message)
    {
    }
    public DeviceConflictException(Device existingDevice, Device suggestedDevice)
    {
        ExistingDevice = existingDevice;
        SuggestedDevice = suggestedDevice;
    }

    public DeviceConflictException(string? message, Device existingDevice, Device suggestedDevice) : base(message)
    {
        ExistingDevice = existingDevice;
        SuggestedDevice = suggestedDevice;
    }

    public DeviceConflictException(string? message, Exception? innerException, Device existingDevice, Device suggestedDevice) : base(message, innerException)
    {
        ExistingDevice = existingDevice;
        SuggestedDevice = suggestedDevice;
    }
}