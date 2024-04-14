namespace AttendanceTrackingSystem.Models
{
    public enum AttendaneStatus
    {
        Absent,
        Late,
        onTime,
        AbsentWithPermission,
        LateWithPermission
    }
    public class Attendance
    {
        
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly CheckIn { get; set; }
        public TimeOnly CheckOut { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public AttendaneStatus Status { get; set; }
    }
}
