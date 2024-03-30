namespace AttendanceTrackingSystem.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public int TrackId { get; set; }
        public virtual Track Track { get; set; }
        public virtual List<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>();
    }
}
