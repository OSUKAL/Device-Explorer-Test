using Test.Exceptions;

namespace Test.Models;

/// <summary>
/// Список устройств
/// </summary>
public class DeviceList
{
    private readonly List<Device> _devices;

    /// <inheritdoc cref="DeviceList"/>
    public DeviceList()
    {
        _devices = [];
    }

    /// <summary>
    /// Получение всех устройств
    /// </summary>
    public IEnumerable<Device> GetAll() => _devices;

    /// <summary>
    /// Добавление нового устройства
    /// </summary>
    /// <param name="device">Данные устройства</param>
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

    /// <summary>
    /// Удаление устройства
    /// </summary>
    /// <param name="device">Данные устройства на удаление</param>
    public void DeleteDevice(Device device)
    {
        _devices.Remove(device);
    }

    /// <summary>
    /// Получение устройства по серийному номеру
    /// </summary>
    /// <param name="serialNumber">Серийный номер</param>
    public Device GetBySerialNumber(string serialNumber)
    {
        return _devices.FirstOrDefault(device => device.SerialNumber == serialNumber);
    }

    /// <summary>
    /// Обновление данных устройства
    /// </summary>
    /// <param name="serialNumber">Серийный номер обновляемого устройства</param>
    /// <param name="updatedDevice">Обновленные данные</param>
    public void UpdateDevice(string serialNumber, Device updatedDevice)
    {
        var deviceToUpdate = _devices.FirstOrDefault(device => device.SerialNumber == serialNumber);
        
        deviceToUpdate.Name = updatedDevice.Name;
        deviceToUpdate.SerialNumber = updatedDevice.SerialNumber;
        deviceToUpdate.Category = updatedDevice.Category;
        deviceToUpdate.Status = updatedDevice.Status;
        deviceToUpdate.InstallationDate = updatedDevice.InstallationDate;
    }

    public void LoadDevices(IEnumerable<Device> deviceList)
    {
        foreach (var device in deviceList)
        {
            _devices.Add(device);
        }
    }
}