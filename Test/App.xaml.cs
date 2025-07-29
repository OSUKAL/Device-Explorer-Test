using System.Windows;
using Test.Models;
using Test.Services;
using Test.Store;
using Test.ViewModels;
using Test.Views;

namespace Test;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore _navigationStore;
    private readonly DeviceList _deviceList;
    
    public App()
    {
        _deviceList = new DeviceList();
        _navigationStore = new NavigationStore();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var savedDeviceList = DeviceListSerializeService.Deserialize();
        if (savedDeviceList != null)
            _deviceList.LoadDevices(savedDeviceList);
        
        _navigationStore.CurrentViewModel = CreateDeviceListViewModel();
        
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        
        MainWindow.Show();
        
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        DeviceListSerializeService.Serialize(_deviceList.GetAll());
        
        base.OnExit(e);
    }

    private AddDeviceViewModel CreateAddDeviceViewModel()
    {
        return new AddDeviceViewModel(_deviceList, new NavigationService(_navigationStore, CreateDeviceListViewModel));
    }

    private DeviceListViewModel CreateDeviceListViewModel()
    {
        return new DeviceListViewModel(_deviceList ,new NavigationService(_navigationStore, CreateAddDeviceViewModel));
    }
}