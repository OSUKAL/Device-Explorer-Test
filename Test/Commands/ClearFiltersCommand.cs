using System.ComponentModel;
using Test.Models;
using Test.ViewModels;

namespace Test.Commands;

public class ClearStatusFilterCommand : CommandBase
{
    private DeviceListViewModel _deviceListViewModel;

    public ClearStatusFilterCommand(DeviceListViewModel deviceListViewModel)
    {
        _deviceListViewModel = deviceListViewModel;

        _deviceListViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _deviceListViewModel.DeviceStatusFilter != null && 
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        _deviceListViewModel.DeviceStatusFilter = null;
        _deviceListViewModel.SelectedDevice = null;
        _deviceListViewModel.EditedDevice = null;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DeviceListViewModel.DeviceStatusFilter))
        {
            OnCanExecuteChanged();
        }
    }
}