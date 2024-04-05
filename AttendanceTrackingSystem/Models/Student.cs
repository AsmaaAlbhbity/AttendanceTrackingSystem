using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public class Student : User
    {
        public int StudentDegree { get; set; }
        public string StudentUniversity { get; set; }
        public string StudentFaculity { get; set; }
        public DateTime StudentGraduationYear { get; set; }
        public string StudentSpecialization { get; set; }
        [Required]
        public int TrackId { get; set; }
        public virtual Track? Track { get; set; }
        public virtual List<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>();


    }
}
