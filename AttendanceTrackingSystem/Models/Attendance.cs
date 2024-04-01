namespace AttendanceTrackingSystem.Models
{
    public class Attendance
    {
        public enum AttendaneStatus
        {
            Absent,
            Late
        }
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly CheckIn { get; set; }
        public TimeOnly CheckOut { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public AttendaneStatus Status { get; set; }
    }
}
