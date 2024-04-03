using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModel
{
    public class ChooseSupervisorViewModel
    {
        public Track Track { get; set; }
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
        public int UserId { get; set; }
    }
}
