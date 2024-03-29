using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoInstructor : IRepoInstructor
    {
        AttendanceDBContext db;
        public RepoInstructor(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Instructor instructor)
        {
            db.Users.Add(instructor);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Users.FirstOrDefault(a => a.UserId == id);
            db.Users.Remove(obj);
            db.SaveChanges();
        }

        public List<Instructor> getAll()
        {
            return db.Users.OfType<Instructor>().ToList();
        }

        public Instructor getById(int id)
        {
            return db.Users.OfType<Instructor>().FirstOrDefault(a => a.UserId == id);
        }

        public void Update(Instructor instructor)
        {
            db.Users.Update(instructor);
            db.SaveChanges();
        }
    }
}
