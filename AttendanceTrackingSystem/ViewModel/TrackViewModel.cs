using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Pagination;

namespace AttendanceTrackingSystem.ViewModel
{
    public class TrackViewModel
    {
        public List<Instructor> Instructors { get; set; }= new List<Instructor>();
        public List<Track> Tracks { get; set; } = new List<Track>();
        public List<Instructor> Supervisour {  get; set; }=new List<Instructor>();
        public PaginatedList<Track> TracksPaination { get; set; }
    }
}
