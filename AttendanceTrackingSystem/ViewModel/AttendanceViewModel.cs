using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModel
{
    public class AttendanceViewModel
    {
        public List<Student>? Students { get; set; }=new List<Student>();
        public List<Track>? Tracks { get; set; }=new List<Track>() ;
        public List<int>? UserId { get; set; }=new List<int>();
        public List<Attendance> Attendances { get; set; } =new List<Attendance>() ;
        public List<User>? Users { get; set; }= new List<User>() ;
    }
}
