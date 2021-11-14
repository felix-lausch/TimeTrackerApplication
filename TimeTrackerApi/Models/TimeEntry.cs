﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TimeTrackerApi.Models
{
    public class TimeEntry
    {
        public Guid Id { get; internal set; } = Guid.NewGuid();

        [JsonIgnore]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string DisplayDate => Date.ToShortDateString();

        public int StartHours { get; set; }

        public int StartMinutes { get; set; }

        [JsonIgnore]
        public double StartMinutesAlt { get; set; } = 0.0;

        [JsonIgnore]
        public TimeOnly StartTime => new (StartHours, StartMinutes, 0);

        public string DisplayStartTime => StartTime.ToShortTimeString();

        public int EndHours { get; set; }

        public int EndMinutes { get; set; }

        [JsonIgnore]
        public double EndMinutesAlt { get; set; } = 0.0;

        [JsonIgnore]
        public TimeOnly EndTime => new(EndHours, EndMinutes, 0);

        public string DisplayEndTime => EndTime.ToShortTimeString();

        public double PauseHours { get; set; } = 0.0;

        public TimeSpan TotalHours => GetTotalTime();

        private TimeSpan GetTotalTime()
        {
            return (EndTime - StartTime) - TimeSpan.FromHours(PauseHours);
        }
    }
}