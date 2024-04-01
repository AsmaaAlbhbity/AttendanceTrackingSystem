namespace AttendanceTrackingSystem.Models
{

    public class StudentAttendance : Attendance
    {
        public int SchduleId { get; set; }
        public virtual Schedule StudentSchdule { get; set; }

    }
}
