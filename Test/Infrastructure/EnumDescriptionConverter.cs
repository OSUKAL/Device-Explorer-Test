using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Test.Infrastructure;

/// <summary>
/// Конвертер для получения описания значений перечисления
/// </summary>
public class EnumDescriptionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || !value.GetType().IsEnum) return string.Empty;
        
        var enumType = value.GetType();
        
        var name = Enum.GetName(enumType, value);
        if (name == null) return string.Empty;
        
        var field = enumType.GetField(name);
        if (field == null) return string.Empty;
        
        var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        
        return attribute?.Description ?? name;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
