namespace TimeTrackerApi.Util;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class DateOnlyValueConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyValueConverter() : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
    { }
}
