namespace AttendanceTrackingSystem.Models
{
    public class Msg
    {
        public int MsgId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public bool IsRead { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
