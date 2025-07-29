using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Test.Commands;
using Test.Models;
using Test.Models.Enums;
using Test.Services;

namespace Test.ViewModels;

/// <summary>
/// Модель представления списка устройств
/// </summary>
public class DeviceListViewModel : ViewModelBase
{
    private readonly DeviceList _deviceList;
    private readonly ObservableCollection<DeviceViewModel> _devices = [];

    private DeviceViewModel? _selectedDevice;
    private DeviceViewModel? _editedDevice;
    private string _deviceNameFilter = string.Empty;
    private DeviceStatus? _deviceStatusFilter = null;

    public IEnumerable<DeviceViewModel> Devices => _devices;

    public static IEnumerable<DeviceCategory> DeviceCategories =>
        Enum.GetValues(typeof(DeviceCategory)).Cast<DeviceCategory>();

    public static IEnumerable<DeviceStatus> DeviceStatuses => Enum.GetValues(typeof(DeviceStatus)).Cast<DeviceStatus>();
    public ICollectionView DevicesCollectionView { get; set; }

    public DeviceStatus? DeviceStatusFilter
    {
        get => _deviceStatusFilter;
        set
        {
            _deviceStatusFilter = value;
            EditedDevice = null;
            OnPropertyChanged();
            DevicesCollectionView?.Refresh();
        }
    }

    public string DeviceNameFilter
    {
        get => _deviceNameFilter;
        set
        {
            _deviceNameFilter = value;
            EditedDevice = null;
            OnPropertyChanged();
            DevicesCollectionView?.Refresh();
        }
    }

    /// <summary>
    /// Выделенное устройство
    /// </summary>
    public DeviceViewModel? SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            _selectedDevice = value;

            if (_selectedDevice != null)
                EditedDevice = new DeviceViewModel(new Device
                {
                    Name = value.Name,
                    Category = value.Category,
                    Status = value.Status,
                    InstallationDate = value.InstallationDate,
                    SerialNumber = value.SerialNumber,
                });
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Данные для просмотра и обновления информации
    /// </summary>
    public DeviceViewModel? EditedDevice
    {
        get => _editedDevice;
        set
        {
            _editedDevice = value;
            OnPropertyChanged();
        }
    }

    public ICommand ClearStatusFilterCommand { get; }
    public ICommand AddDeviceCommand { get; }
    public ICommand RemoveDeviceCommand { get; }
    public ICommand SaveDeviceChangesCommand { get; }

    /// <inheritdoc cref="DeviceListViewModel"/>
    public DeviceListViewModel(DeviceList deviceList, NavigationService addDeviceNavigationService)
    {
        _deviceList = deviceList;

        AddDeviceCommand = new NavigateCommand(addDeviceNavigationService);
        RemoveDeviceCommand = new RemoveDeviceCommand(this, _deviceList);
        SaveDeviceChangesCommand = new EditDeviceCommand(this, _deviceList);
        ClearStatusFilterCommand = new ClearFiltersCommand(this);

        DevicesCollectionView = CollectionViewSource.GetDefaultView(_devices);
        DevicesCollectionView.Filter = FilterDevices;
        DevicesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(DeviceViewModel.Category)));
        
        UpdateDevices();
    }

    private bool FilterDevices(object obj)
    {
        if (obj is not DeviceViewModel device)
            return false;

        if (DeviceNameFilter != string.Empty && DeviceStatusFilter.HasValue)
            return device.Name.Contains(DeviceNameFilter, StringComparison.OrdinalIgnoreCase) && device.Status.Equals(DeviceStatusFilter);
        
        if (DeviceNameFilter != string.Empty)
            return device.Name.Contains(DeviceNameFilter, StringComparison.OrdinalIgnoreCase);

        if (DeviceStatusFilter.HasValue)
            return device.Status.Equals(DeviceStatusFilter);

        return true;
    }

    /// <summary>
    /// Обновление списка устройств
    /// </summary>
    public void UpdateDevices()
    {
        _devices.Clear();

        foreach (var device in _deviceList.GetAll())
        {
            var deviceViewModel = new DeviceViewModel(device);
            _devices.Add(deviceViewModel);
        }

        DevicesCollectionView = CollectionViewSource.GetDefaultView(_devices);
    }
}