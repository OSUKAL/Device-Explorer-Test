using Test.Models.Enums;

namespace Test.Models;

/// <summary>
/// Данные устройства
/// </summary>
public class Device
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public DeviceStatus Status { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public DeviceCategory Category { get; set; }

    /// <summary>
    /// Серийный номер
    /// </summary>
    public string SerialNumber { get; set; }

    /// <summary>
    /// Дата установки
    /// </summary>
    public DateTime InstallationDate { get; set; }

    public bool Conflicts(Device device)
    {
        return device.SerialNumber == SerialNumber;
    }
    
    public override bool Equals(object obj)
    {
        return obj is Device device && 
               Name == device.Name &&
               SerialNumber == device.SerialNumber &&
               Status == device.Status &&
               Category == device.Category &&
               InstallationDate == device.InstallationDate;
        
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, (int)Status, (int)Category, SerialNumber, InstallationDate);
    }
}