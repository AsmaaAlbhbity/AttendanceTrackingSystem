using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModel
{
    public class ShowInstructorViewModel
    {
        public List<Instructor> Instroctors { get; set; } = new List<Instructor>();
        public List<Track> Tracks { get; set; } = new List<Track>();
        public User User { get; set; }
    }
}
