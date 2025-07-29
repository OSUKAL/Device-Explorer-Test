using System.Windows.Input;
using Test.Command;
using Test.Common.Enums;
using Test.Model;
using Test.Store;

namespace Test.ViewModel;

public class AddDeviceViewModel : ViewModelBase
{
    private string _name;
    private string _serialNumber;
    private DeviceCategory _category = DeviceCategory.Other;
    private DeviceStatus _status = DeviceStatus.Working;
    private DateTime _installationDate = new DateTime(2025, 7, 28);
    
    public IEnumerable<DeviceCategory> DeviceCategories => Enum.GetValues(typeof(DeviceCategory)).Cast<DeviceCategory>();
    public IEnumerable<DeviceStatus> DeviceStatuses => Enum.GetValues(typeof(DeviceStatus)).Cast<DeviceStatus>();

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string SerialNumber
    {
        get => _serialNumber;
        set
        {
            _serialNumber = value;
            OnPropertyChanged();
        }
    }
    
    public DeviceCategory Category
    {
        get => _category;
        set
        {
            _category = value;
            OnPropertyChanged();
        }
    }
    
    public DeviceStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
        }
    }
    
    public DateTime InstallationDate
    {
        get => _installationDate;
        set
        {
            _installationDate = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public AddDeviceViewModel(DeviceList deviceList, NavigationStore navigationStore, Func<DeviceListViewModel> createDeviceListViewModel)
    {
        SubmitCommand = new AddDeviceCommand(this, deviceList);
        CancelCommand = new NavigateCommand(navigationStore, createDeviceListViewModel);
    }
}