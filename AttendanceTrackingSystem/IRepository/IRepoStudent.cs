using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoStudent
    {
        public List<Student> getAll();
        public Student getById(int id);
        public void Add(Student student);
        public void Update(Student student);
        public void Delete(int id);

        public List<Student> GetPendingStudents();
        public string GetTrackNameByUserId(int userId);
        public Instructor GetSupervisorByStudentId(int userId);
        public void ApproveStudent(int studentId);

        public void RejectStudent(int studentId);

        public List<Schedule> GetFutureStudentSchedule(int studentId);
        public List<Student> GetPaginatedStudents(int page, int pageSize);

    }
}
