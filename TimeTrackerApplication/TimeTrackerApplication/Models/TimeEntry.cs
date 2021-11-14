namespace TimeTrackerApplication.Models
{
    public class TimeEntry
    {
        public string DisplayStartTime { get; set; }

        public string DisplayEndTime { get; set; }

        public double PauseHours { get; set; } = 0.0;

        public string TotalHours { get; set; }
    }
}
