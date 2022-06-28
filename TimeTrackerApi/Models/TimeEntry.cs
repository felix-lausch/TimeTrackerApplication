﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace TimeTrackerApi.Models
{
    public class TimeEntry
    {
        public Guid Id { get; internal set; }

        [JsonIgnore]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string DisplayDate => Date.ToShortDateString();

        public string DisplayDate_US => Date.ToString("yyyy-MM-dd");

        public string Weekday => Date.ToString("dddd", CultureInfo.InvariantCulture).ToUpper();

        public int StartHours { get; set; }

        public int StartMinutes { get; set; }

        [JsonIgnore]
        public double StartMinutesAlt { get; set; } = 0.0;

        [JsonIgnore]
        public TimeOnly StartTime => TryParseTimeOnly(StartHours, StartMinutes, 0);

        public string DisplayStartTime => StartTime.ToShortTimeString();

        public int EndHours { get; set; }

        public int EndMinutes { get; set; }

        [JsonIgnore]
        public double EndMinutesAlt { get; set; } = 0.0;

        [JsonIgnore]
        public TimeOnly EndTime => TryParseTimeOnly(EndHours, EndMinutes, 0);

        public string DisplayEndTime => EndTime.ToShortTimeString();

        public double PauseHours { get; set; } = 0.0;

        public TimeSpan TotalHours => (EndTime - StartTime) - TimeSpan.FromHours(PauseHours);

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
}
