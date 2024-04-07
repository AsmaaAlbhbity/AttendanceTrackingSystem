
using AttendanceTrackingSystem.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AttendanceTrackingSystem.Models
{
    public class Student : User
    {
        //[Range(1,250,ErrorMessage = $"{nameof(StudentDegree)} must be between 1 and 250.")]
        public int StudentDegree { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = $"{nameof(StudentUniversity)} must be at least 3 characters long.")]
        public string StudentUniversity { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = $"{nameof(StudentFaculity)} must be at least 3 characters long.")]
        public string StudentFaculity { get; set; }
        //[GraduationYear(10)]
        public DateTime StudentGraduationYear { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = $"{nameof(StudentSpecialization)} must be at least 2 characters long.")]
        public string StudentSpecialization { get; set; }
        [Required]
        public int TrackId { get; set; }
        public virtual Track Track { get; set; }
        public virtual List<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>();


    }
}
