using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Track { 
        public int TrackId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public virtual List<Instructor> Instructors { get; set; } = new List<Instructor>();
        public virtual List<Student> Students { get; set; } = new List<Student>();
        [ForeignKey("Instructor")]
        public int SupervisorId { get; set; }
        public virtual User Instructor { get; set; } 
        public int IntakeId { get; set; }
        public virtual Intake Intake {  get; set; }
        public bool IsActive { get; set; }
    }
}
