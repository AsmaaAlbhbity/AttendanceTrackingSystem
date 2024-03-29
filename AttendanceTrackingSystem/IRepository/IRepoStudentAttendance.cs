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
    }
}
