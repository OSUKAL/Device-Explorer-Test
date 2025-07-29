using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Test.Models;

namespace Test.Services;

/// <summary>
/// Сервис для работы с JSON
/// </summary>
public static class DeviceListSerializeService
{
    private static readonly string DestinationDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName + @"\deviceList.json";
    
    /// <summary>
    /// Сериализация списка устройств
    /// </summary>
    /// <param name="deviceList">Список устройств</param>
    public static void Serialize(IEnumerable<Device> deviceList)
    {
        var json = JsonSerializer.Serialize(
            deviceList,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            });
        File.WriteAllText(DestinationDirectory, json);
    }

    /// <summary>
    /// Десериализация списка устройств
    /// </summary>
    public static IEnumerable<Device> Deserialize()
    {
        if (!File.Exists(DestinationDirectory)) return [];

        var json = File.ReadAllText(DestinationDirectory);
        if (string.IsNullOrEmpty(json)) return [];

        return JsonSerializer.Deserialize<IEnumerable<Device>>(json) ?? [];
    }
}