using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModel
{
    public class AttendanceSummaryViewModel
    {
        public int UserId { get; set; }
        public int LateCount { get; set; }
        public int OnTimeCount { get; set; }
        public int AbsentCount { get; set; }
       public List<Msg> userMessages {  get; set; }
        public DateTime StartDate { get; set; }
        public List<Attendance>? AttendanceData { get; set; }

        public List<StudentAttendance>? AttendanceStudentData { get; set; }


        public List<Schedule>?  StudentSchedule { get; set; }
       public List<Permission>? UserPermissions { get; set; }
    }
}
