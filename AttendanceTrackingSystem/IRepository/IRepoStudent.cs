using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoStudent
    {
        public List<Student> getAll();
        public Student getById(int id);
        public void Add(Student student);
        public void Update(Student student);
        public void Delete(int id);

        public string GetTrackNameByUserId(int userId);
        public string GetSupervisorNameByUserId(int userId);
        public List<Schedule> GetFutureStudentSchedule(int studentId);
        public List<Student> GetPaginatedStudents(int page, int pageSize);
        List<Attendance> GetStudentAttendance(DateTime selectedDate, int selectedTrackId);
    }

}

