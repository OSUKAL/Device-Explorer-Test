using System.ComponentModel;
using Test.ViewModels;

namespace Test.Commands;

public class ClearFiltersCommand : CommandBase
{
    private DeviceListViewModel _deviceListViewModel;

    public ClearFiltersCommand(DeviceListViewModel deviceListViewModel)
    {
        _deviceListViewModel = deviceListViewModel;

        _deviceListViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return (_deviceListViewModel.DeviceStatusFilter != null ||
               _deviceListViewModel.DeviceNameFilter != string.Empty) &&
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        _deviceListViewModel.DeviceNameFilter = string.Empty;
        _deviceListViewModel.DeviceStatusFilter = null;
        _deviceListViewModel.SelectedDevice = null;
        _deviceListViewModel.EditedDevice = null;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DeviceListViewModel.DeviceStatusFilter) || 
            e.PropertyName == nameof(DeviceListViewModel.DeviceNameFilter))
        {
            OnCanExecuteChanged();
        }
    }
}