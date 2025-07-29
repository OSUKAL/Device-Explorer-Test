using System.Windows.Input;
using Test.Commands;
using Test.Models;
using Test.Models.Enums;
using Test.Services;

namespace Test.ViewModels;

/// <summary>
/// Модель представления добавления устройства
/// </summary>
public class AddDeviceViewModel : ViewModelBase
{
    private string _name;
    private string _serialNumber;
    private DeviceCategory _category = DeviceCategory.Other;
    private DeviceStatus _status = DeviceStatus.Working;
    private DateTime _installationDate = DateTime.Now;
    
    public IEnumerable<DeviceCategory> DeviceCategories => Enum.GetValues(typeof(DeviceCategory)).Cast<DeviceCategory>();
    public IEnumerable<DeviceStatus> DeviceStatuses => Enum.GetValues(typeof(DeviceStatus)).Cast<DeviceStatus>();

    /// <summary>
    /// Название
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Серийный номер
    /// </summary>
    public string SerialNumber
    {
        get => _serialNumber;
        set
        {
            _serialNumber = value;
            OnPropertyChanged();
        }
    }
    
    /// <summary>
    /// Категория
    /// </summary>
    public DeviceCategory Category
    {
        get => _category;
        set
        {
            _category = value;
            OnPropertyChanged();
        }
    }
    
    /// <summary>
    /// Статус
    /// </summary>
    public DeviceStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
        }
    }
    
    /// <summary>
    /// Время установки
    /// </summary>
    public DateTime InstallationDate
    {
        get => _installationDate;
        set
        {
            _installationDate = value;
            OnPropertyChanged();
        }
    }
    
    /// <summary>
    /// Команда добавления устройтва
    /// </summary>
    public ICommand SubmitCommand { get; }
    /// <summary>
    /// Команда отмены добавления 
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <inheritdoc cref="AddDeviceViewModel"/>
    public AddDeviceViewModel(DeviceList deviceList, NavigationService deviceListViewNavigationService)
    {
        SubmitCommand = new AddDeviceCommand(this, deviceList, deviceListViewNavigationService);
        CancelCommand = new NavigateCommand(deviceListViewNavigationService);
    }
}