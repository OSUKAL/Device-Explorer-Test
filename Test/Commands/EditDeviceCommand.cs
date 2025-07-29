using System.ComponentModel;
using System.Windows;
using Test.Exceptions;
using Test.Models;
using Test.ViewModels;

namespace Test.Commands;

public class EditDeviceCommand : CommandBase
{
    private DeviceListViewModel _deviceListViewModel;
    private DeviceList _deviceList;

    public EditDeviceCommand(DeviceListViewModel deviceListViewModel, DeviceList deviceList)
    {
        _deviceListViewModel = deviceListViewModel;
        _deviceList = deviceList;

        _deviceListViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _deviceListViewModel.SelectedDevice != null &&
               _deviceListViewModel.EditedDevice != null &&
               !string.IsNullOrWhiteSpace(_deviceListViewModel.EditedDevice.SerialNumber) &&
               !string.IsNullOrWhiteSpace(_deviceListViewModel.EditedDevice.Name) &&
               !(_deviceListViewModel.EditedDevice.InstallationDate >= DateTime.Now) &&
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        try
        {
            var selectedDevice = _deviceListViewModel.SelectedDevice;
            var editedDevice = _deviceListViewModel.EditedDevice;
            
            if (_deviceList.GetBySerialNumber(editedDevice.SerialNumber) != null &&
                selectedDevice.SerialNumber != editedDevice.SerialNumber)
            {
                throw new DeviceConflictException("Устройство с таким серийным номером уже существует");
            }
            
            _deviceList.UpdateDevice(selectedDevice.SerialNumber, new Device
            {
                Name = editedDevice.Name,
                Category = editedDevice.Category,
                Status = editedDevice.Status,
                InstallationDate = editedDevice.InstallationDate,
                SerialNumber = editedDevice.SerialNumber
            });
            
            MessageBox.Show(
                "Изменения сохранены",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            
            _deviceListViewModel.UpdateDevices();
            _deviceListViewModel.SelectedDevice = _deviceListViewModel.Devices.FirstOrDefault(device => device.SerialNumber == editedDevice.SerialNumber);
        }
        catch (DeviceConflictException exception)
        {
            var selectedDevice = _deviceListViewModel.SelectedDevice;
            
            MessageBox.Show(
                exception.Message,
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            _deviceListViewModel.SelectedDevice = selectedDevice;
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DeviceListViewModel.EditedDevice.SerialNumber) ||
            e.PropertyName == nameof(DeviceListViewModel.EditedDevice.InstallationDate) ||
            e.PropertyName == nameof(DeviceListViewModel.EditedDevice.Name) ||
            e.PropertyName == nameof(DeviceListViewModel.SelectedDevice))
        {
            OnCanExecuteChanged();
        }
    }
}