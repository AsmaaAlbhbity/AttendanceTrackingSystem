namespace AttendanceTrackingSystem.Models
{
    public class Instructor :User
    {
        public decimal InstructorSalary {  get; set; } 
        public virtual List<Track> Tracks { get; set; }= new List<Track>();
        public virtual List<Attendance> InstructorAttendances { get; set; } = new List<Attendance>();

    }
}
