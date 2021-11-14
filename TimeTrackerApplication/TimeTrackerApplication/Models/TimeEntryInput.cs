namespace TimeTrackerApi.Models
{
    public class TimeEntryInput
    {
        public int StartHours { get; set; }

        public int StartMinutes { get; set; }

        //public double StartMinutesAlt { get; set; } = 0.0;

        public int EndHours { get; set; }

        public int EndMinutes { get; set; }

        //public double EndMinutesAlt { get; set; } = 0.0;

        public double PauseHours { get; set; } = 0.0;
    }
}
