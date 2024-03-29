using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoIntake : IRepoIntake
    {
        AttendanceDBContext db;
        public RepoIntake(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Intake intake)
        {
            db.Intake.Add(intake);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Intake.FirstOrDefault(a => a.IntakeId == id);
            db.Intake.Remove(obj);
            db.SaveChanges();
        }

        public List<Intake> getAll()
        {
            return db.Intake.ToList();
        }

        public Intake getById(int id)
        {
            return db.Intake.FirstOrDefault(a => a.IntakeId == id);
        }

        public void Update(Intake intake)
        {
            db.Intake.Update(intake);
            db.SaveChanges();
        }
    }
}
