using Test.Common.Enums;

namespace Test.Model;

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
}