using Test.Models;
using Test.Models.Enums;

namespace Test.ViewModels;

/// <summary>
/// Модель представления данных устройства
/// </summary>
public class DeviceViewModel : ViewModelBase
{
    private readonly Device _device;

    public string Name
    {
        get => _device.Name;
        set
        {
            _device.Name = value;
            OnPropertyChanged();
        }
    }

    public string SerialNumber
    {
        get => _device.SerialNumber;
        set
        {
            _device.SerialNumber = value;
            OnPropertyChanged();
        }
    }

    public DeviceCategory Category
    {
        get => _device.Category;
        set
        {
            _device.Category = value;
            OnPropertyChanged();
        }
    }

    public DeviceStatus Status
    {
        get => _device.Status;
        set
        {
            _device.Status = value;
            OnPropertyChanged();
        }
    }

    public DateTime InstallationDate
    {
        get => _device.InstallationDate;
        set
        {
            _device.InstallationDate = value;
            OnPropertyChanged();
        }
    }

    public DeviceViewModel(Device device)
    {
        _device = device;
    }
}