using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoAttendance : IRepoAttendance
    {
        AttendanceDBContext db;
        public RepoAttendance(AttendanceDBContext _db)
        {
            db= _db;
        }

        public void Add(Attendance attendance)
        {
            db.Attendances.Add(attendance);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Attendances.FirstOrDefault(a => a.AttendanceId == id);
            db.Attendances.Remove(obj);
            db.SaveChanges();
        }

        public List<Attendance> getAll()
        {
            return db.Attendances.ToList();
        }

        public Attendance getById(int id)
        {
            return db.Attendances.FirstOrDefault(a=>a.AttendanceId==id);
        }

        public void Update(Attendance attendance)
        {
            db.Attendances.Update(attendance);
            db.SaveChanges();
        }
    }
}
