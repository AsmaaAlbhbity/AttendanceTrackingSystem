using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Pagination;

namespace AttendanceTrackingSystem.ViewModel
{
    public class ShowInstructorViewModel
    {
        public List<Instructor> Instroctors { get; set; } = new List<Instructor>();
        public List<Track> Tracks { get; set; } = new List<Track>();
        public User User { get; set; }
        public PaginatedList<Instructor> InstructorsPaination { get; set; }
    }
}
