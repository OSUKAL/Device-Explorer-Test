using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Test.Infrastructure;

public class EnumAttributeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return string.Empty;
        
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
        if (value is string stringValue && targetType.IsEnum)
        {
            foreach (Enum enumItem in Enum.GetValues(targetType))
            {
                if (GetDescription(enumItem) == stringValue)
                {
                    return enumItem;
                }
            }
        }
        return DependencyProperty.UnsetValue;
    }
    
    private static string GetDescription(Enum value)
    {
        var typeInfo = value.GetType();
        var field = typeInfo.GetField(value.ToString());
        var descriptionAttribute = field!.GetCustomAttribute<DescriptionAttribute>();

        var description = descriptionAttribute?.Description ?? string.Empty;

        return description;
    }
}
