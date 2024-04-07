namespace AttendanceTrackingSystem.Models
{

    public class StudentAttendance : Attendance
    {
        public int? currentDegree {  get; set; }
        public int? minDegree {  get; set; }
        public int? SchduleId { get; set; }
        public virtual Schedule StudentSchdule { get; set; }

    }
}
