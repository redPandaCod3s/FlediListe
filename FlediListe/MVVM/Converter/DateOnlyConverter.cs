using System.Globalization;

namespace FlediListe.MVVM.Converter;

public class DateOnlyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is DateOnly dateOnly)
            return dateOnly.ToDateTime(TimeOnly.MinValue);
        return DateTime.Now;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is DateTime dateTime)
            return DateOnly.FromDateTime(dateTime);
        return DateOnly.FromDateTime(DateTime.Now);
    }
}