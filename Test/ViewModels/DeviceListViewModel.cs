using System.Collections.ObjectModel;
using System.Windows.Input;
using Test.Command;
using Test.Common.Enums;
using Test.Model;
using Test.Store;

namespace Test.ViewModel;

public class DeviceListViewModel : ViewModelBase
{
    private readonly ObservableCollection<DeviceViewModel> _devices;
    private DeviceViewModel _selectedDevice;

    public IEnumerable<DeviceViewModel> Devices => _devices;

    public IEnumerable<DeviceCategory> DeviceCategories => Enum.GetValues(typeof(DeviceCategory)).Cast<DeviceCategory>();
    public IEnumerable<DeviceStatus> DeviceStatuses => Enum.GetValues(typeof(DeviceStatus)).Cast<DeviceStatus>();

    public DeviceViewModel SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            _selectedDevice = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddDeviceCommand { get; }
    public ICommand RemoveDeviceCommand { get; }
    public ICommand SaveDeviceChangesCommand { get; }

    public DeviceListViewModel(NavigationStore navigationStore, Func<AddDeviceViewModel> createAddDeviceViewModel)
    {
        _devices = new ObservableCollection<DeviceViewModel>();

        AddDeviceCommand = new NavigateCommand(navigationStore, createAddDeviceViewModel);
        
        _devices.Add(new DeviceViewModel(
            new Device
            {
                Name = "Беспроводная мышь Ardor Gaming",
                Status = DeviceStatus.Working,
                Category = DeviceCategory.Other,
                SerialNumber = "SDAE123145ASD13",
                InstallationDate = DateTime.Now
            }));
        _devices.Add(new DeviceViewModel(
            new Device
            {
                Name = "Сервер Aternos",
                Status = DeviceStatus.Working,
                Category = DeviceCategory.Server,
                SerialNumber = "412D1231D123441",
                InstallationDate = DateTime.Now
            }));

        _devices.Add(new DeviceViewModel(
            new Device
            {
                Name = "Монитор Iiyama 27\"",
                Status = DeviceStatus.Broken,
                Category = DeviceCategory.Other,
                SerialNumber = "123141ASDFA12313",
                InstallationDate = DateTime.Now
            }));
    }
}