using System.ComponentModel;

namespace Test.Models.Enums;

/// <summary>
/// Статус устройства
/// </summary>
public enum DeviceStatus
{
    /// <summary>
    /// Работает
    /// </summary>
    [Description("Работает")]
    Working = 0,
    
    /// <summary>
    /// Сломано
    /// </summary>
    [Description("Сломано")]
    Broken = 1,
    
    /// <summary>
    /// Списано
    /// </summary>
    [Description("Списано")]
    WrittenOff = 2,
}