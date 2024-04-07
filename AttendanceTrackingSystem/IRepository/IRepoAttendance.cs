using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public class AttendanceCountPerUserType
    {
        public string UserType { get; set; }
        public int AttendanceCount { get; set; }
        public double AttendancePercentage { get; set; }
    }
    public interface IRepoAttendance
    {
        public List<Attendance> getAll();
        public Attendance getById(int id);
        public void Add(Attendance attendance);
        public void Update(Attendance attendance);
        public void Delete(int id);

        public List<Attendance> GetUserAttendance(int userId, DateTime startDate, DateTime endDate);
        public List<AttendanceCountPerUserType> GetAttendanceCountsPerUserType();
        public List<int> GetEmployeeAttendanceForToday();
        public List<int> GetInstructorAttendanceForToday();
    }
}
