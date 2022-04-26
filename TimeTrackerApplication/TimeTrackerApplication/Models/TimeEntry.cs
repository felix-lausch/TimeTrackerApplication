namespace TimeTrackerApplication.Models
{
    using System;

    public class TimeEntry
    {
        public Guid Id { get; set; }

        public int StartHours { get; set; }

        public int StartMinutes { get; set; }

        //public double StartMinutesAlt { get; set; } = 0.0;

        public int EndHours { get; set; }

        public int EndMinutes { get; set; }

        //public double EndMinutesAlt { get; set; } = 0.0;

        public double PauseHours { get; set; } = 0.0;

        public string DisplayStartTime { get; set; }

        public string DisplayEndTime { get; set; }

        public string TotalHours { get; set; }
    }
}
