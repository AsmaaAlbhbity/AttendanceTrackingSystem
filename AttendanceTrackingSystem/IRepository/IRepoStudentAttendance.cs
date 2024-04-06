using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoStudentAttendance
    {
        public List<StudentAttendance> getAll();
        public StudentAttendance getById(int id);
        public void Add(StudentAttendance studentAttendance);
        public void Update(StudentAttendance studentAttendance);


        public void Delete(int id);

        public List<StudentAttendance> GetStudentAttendance(int studentId, DateTime startDate, DateTime endDate);

        public List<DateTime> GetStudentScheduleDates(int studentId, DateTime startDate, DateTime endDate);


    }
}
