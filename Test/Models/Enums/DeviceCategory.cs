using System.ComponentModel;

namespace Test.Models.Enums;

/// <summary>
/// Категория устройства
/// </summary>
public enum DeviceCategory
{
    /// <summary>
    /// Другое
    /// </summary>
    [Description("Периферия")]
    Other = 0,
    
    /// <summary>
    /// Сервер
    /// </summary>
    [Description("Сервер")]
    Server = 1,
    
    /// <summary>
    /// Принтер
    /// </summary>
    [Description("Принтер")]
    Printer = 2,
    
    /// <summary>
    /// ПК
    /// </summary>
    [Description("ПК")]
    PC = 3,
}