namespace AttendanceTrackingSystem.Models
{
    public enum AttendaneStatus
    {
        Absent,
        Late
    }

    public class StudentAttendance : Attendance
    {
        public AttendaneStatus Status { get; set; }
        public int SchduleId { get; set; }
        public virtual Schedule StudentSchdule { get; set; }

    }
}
