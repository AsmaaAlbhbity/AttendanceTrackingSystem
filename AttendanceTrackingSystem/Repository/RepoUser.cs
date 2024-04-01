using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoUser : IRepoUser
    {
        AttendanceDBContext db;
        public RepoUser(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Users.FirstOrDefault(a => a.UserId == id);
            db.Users.Remove(obj);
            db.SaveChanges();
        }

        public bool EmailIsUnique(string email)
        {
            return db.Users.Any(a => a.Email == email);
        }

        public List<User> getAll()
        {
            return db.Users.ToList();
        }

        public User getById(int id)
        {
            return db.Users.FirstOrDefault(a => a.UserId == id);
        }

        public void Update(User user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}
