using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;

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

        public List<AttendanceRecordViewModel> GetLateOrAbsentDates(int userId);
        public List<Attendance> GetUserAttendance(int userId, DateTime startDate, DateTime endDate);
        public List<AttendanceCountPerUserType> GetAttendanceCountsPerUserType();
    }
}
