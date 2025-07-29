using System.ComponentModel;
using System.Windows;
using Test.Models;
using Test.ViewModels;

namespace Test.Commands.DeviceList;

public class RemoveDeviceCommand : CommandBase
{
    private readonly DeviceListViewModel _deviceListViewModel;
    private readonly DeviceList _deviceList;

    public RemoveDeviceCommand(DeviceListViewModel deviceListViewModel, DeviceList deviceList)
    {
        _deviceListViewModel = deviceListViewModel;
        _deviceList = deviceList;

        _deviceListViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _deviceListViewModel.SelectedDevice != null;
    }
    
    public override void Execute(object? parameter)
    {
        var confirm = MessageBox.Show("Удалить выбранное устройство?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirm == MessageBoxResult.No) return;
        
        var deviceToDelete = new Device
        {
            Name = _deviceListViewModel.SelectedDevice.Name,
            SerialNumber = _deviceListViewModel.SelectedDevice.SerialNumber,
            Category = _deviceListViewModel.SelectedDevice.Category,
            Status = _deviceListViewModel.SelectedDevice.Status,
            InstallationDate = _deviceListViewModel.SelectedDevice.InstallationDate
        };
        
        _deviceList.DeleteDevice(deviceToDelete);
        _deviceListViewModel.EditedDevice = null;
        _deviceListViewModel.UpdateDevices();
    }
    
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DeviceListViewModel.SelectedDevice))
        {
            OnCanExecuteChanged();
        }
    }
}