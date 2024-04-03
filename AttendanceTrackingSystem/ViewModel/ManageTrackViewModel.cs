using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModel
{
    public class ManageTrackViewModel
    {
        public Instructor Instructor { get; set; }
        public List<Track> InstructorTrack {  get; set; }=new List<Track>();
        public List<Track> AnotherTrack { get; set; } = new List<Track>();
    }
}
