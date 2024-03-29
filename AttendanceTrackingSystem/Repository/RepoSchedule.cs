using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using System.Security;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoSchedule : IRepoSchedule
    {
        AttendanceDBContext db;
        public RepoSchedule(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Schedule schedule)
        {
            db.Schedules.Add(schedule);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Schedules.FirstOrDefault(a => a.ScheduleId == id);
            db.Schedules.Remove(obj);
            db.SaveChanges();
        }

        public List<Schedule> getAll()
        {
            return db.Schedules.ToList();
        }

        public Schedule getById(int id)
        {
            return db.Schedules.FirstOrDefault(a => a.ScheduleId == id);
        }

        public void Update(Schedule schedule)
        {
            db.Schedules.Update(schedule);
            db.SaveChanges();
        }
    }
}
