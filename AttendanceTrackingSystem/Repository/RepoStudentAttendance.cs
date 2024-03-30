using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoStudentAttendance : IRepoStudentAttendance
    {
        AttendanceDBContext db;
        public RepoStudentAttendance(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(StudentAttendance studentAttendance)
        {
            db.StudentAttendances.Add(studentAttendance);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.StudentAttendances.FirstOrDefault(a => a.AttendanceId == id);
            db.StudentAttendances.Remove(obj);
            db.SaveChanges();
        }

        public List<StudentAttendance> getAll()
        {
            return db.StudentAttendances.ToList();
        }

        public StudentAttendance getById(int id)
        {
            return db.StudentAttendances.FirstOrDefault(a => a.AttendanceId == id);
        }

        public void Update(StudentAttendance studentAttendance)
        {
            db.StudentAttendances.Update(studentAttendance);
            db.SaveChanges();
        }
    }
}
