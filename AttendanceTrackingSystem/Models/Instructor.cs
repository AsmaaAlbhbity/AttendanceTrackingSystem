using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public class Instructor :User
    {
        [Range(2000.00, double.MaxValue, ErrorMessage = "Instructor salary must be more than 2000.")]
        public decimal InstructorSalary {  get; set; } 
        public virtual List<Track> Tracks { get; set; }= new List<Track>();
        public virtual List<Attendance> InstructorAttendances { get; set; } = new List<Attendance>();
    }
}
