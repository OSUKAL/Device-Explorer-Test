using System.ComponentModel;
using System.Windows;
using Test.Exceptions;
using Test.Models;
using Test.Services;
using Test.ViewModels;

namespace Test.Commands;

public class AddDeviceCommand : CommandBase
{
    private readonly AddDeviceViewModel _addDeviceViewModel;
    private readonly DeviceList _deviceList;
    private readonly NavigationService _deviceListViewNavigationService;

    public AddDeviceCommand(
        AddDeviceViewModel addDeviceViewModel,
        DeviceList deviceList,
        NavigationService deviceListViewNavigationService
        )
    {
        _addDeviceViewModel = addDeviceViewModel;
        _deviceList = deviceList;
        _deviceListViewNavigationService = deviceListViewNavigationService;

        _addDeviceViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(_addDeviceViewModel.Name) &&
               !string.IsNullOrWhiteSpace(_addDeviceViewModel.SerialNumber) &&
               !(_addDeviceViewModel.InstallationDate >= DateTime.Now) &&
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        var device = new Device
        {
            Name = _addDeviceViewModel.Name,
            SerialNumber = _addDeviceViewModel.SerialNumber,
            InstallationDate = _addDeviceViewModel.InstallationDate,
            Category = _addDeviceViewModel.Category,
            Status = _addDeviceViewModel.Status,
        };

        try
        {
            _deviceList.AddDevice(device);
            
            MessageBox.Show(
                "Устройство добавлено",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            
            _deviceListViewNavigationService.Navigate();
        }
        catch (DeviceConflictException)
        {
            MessageBox.Show(
                "Устройство с таким серийным номером уже существует",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }
    
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AddDeviceViewModel.Name) ||
            e.PropertyName == nameof(AddDeviceViewModel.SerialNumber) ||
            e.PropertyName == nameof(AddDeviceViewModel.InstallationDate))
        {
            OnCanExecuteChanged();
        }
    }
}