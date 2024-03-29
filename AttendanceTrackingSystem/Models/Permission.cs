namespace AttendanceTrackingSystem.Models
{
    public enum PermissionState
    {
        Pending,
        Approved,
        Rejected
    }
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public PermissionState State { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
