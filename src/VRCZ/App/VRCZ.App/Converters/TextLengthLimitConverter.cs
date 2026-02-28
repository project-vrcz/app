using System.Globalization;
using Avalonia.Data.Converters;

namespace VRCZ.App.Converters;

public sealed class TextLengthLimitConverter : IValueConverter
{
    private const int MaxTextLength = 1000;
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string stringValue)
            return null;

        return stringValue.Length > MaxTextLength
            ? stringValue.Substring(0, MaxTextLength) + "..."
            : stringValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}