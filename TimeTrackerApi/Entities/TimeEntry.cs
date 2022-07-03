namespace TimeTrackerApi.Models;

using System.Globalization;
using System.Text.Json.Serialization;


public class TimeEntry
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public int StartHours { get; set; }

    public int StartMinutes { get; set; }

    [JsonIgnore]
    public TimeOnly StartTime => TryParseTimeOnly(StartHours, StartMinutes, 0);

    public string DisplayStartTime => StartTime.ToShortTimeString();

    public int EndHours { get; set; }

    public int EndMinutes { get; set; }

    [JsonIgnore]
    public TimeOnly EndTime => TryParseTimeOnly(EndHours, EndMinutes, 0);

    public string DisplayEndTime => EndTime.ToShortTimeString();

    public double PauseHours { get; set; } = 0.0;

    public TimeSpan TotalHours => (EndTime - StartTime) - TimeSpan.FromHours(PauseHours);

    public void Map(TimeEntry mapFrom)
    {
        Id = mapFrom.Id;
        Date = mapFrom.Date;
        StartHours = mapFrom.StartHours;
        StartMinutes = mapFrom.StartMinutes;
        EndHours = mapFrom.EndHours;
        EndMinutes = mapFrom.EndMinutes;
        PauseHours = mapFrom.PauseHours;
    }

    private static TimeOnly TryParseTimeOnly(int hour, int minute, int second)
    {
        try
        {
            var timeOnly = new TimeOnly(hour, minute, second);
            return timeOnly;
        }
        catch
        {
            return TimeOnly.MinValue;
        }
    }
}
