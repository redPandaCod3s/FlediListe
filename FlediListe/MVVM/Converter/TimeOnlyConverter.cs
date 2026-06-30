using System.Globalization;

namespace FlediListe.MVVM.Converter;

public class TimeOnlyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeOnly timeOnly)
            return timeOnly.ToTimeSpan();
        return TimeSpan.Zero;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan)
            return TimeOnly.FromTimeSpan(timeSpan);
        return null;
    }
}