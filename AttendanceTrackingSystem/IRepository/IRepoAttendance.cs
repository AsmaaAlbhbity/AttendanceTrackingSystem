using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoAttendance
    {
        public List<Attendance> getAll();
        public Attendance getById(int id);
        public void Add(Attendance attendance);
        public void Update(Attendance attendance);
        public void Delete(int id);
    }
}
