using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoEmployee : IRepoEmployee
    {
        AttendanceDBContext db;
        public RepoEmployee(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Employee employee)
        {
            db.Users.Add(employee);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Users.FirstOrDefault(a => a.UserId == id);
            db.Users.Remove(obj);
            db.SaveChanges();
        }

        public List<Employee> getAll()
        {
            return db.Users.OfType<Employee>().ToList();
        }

        public Employee getById(int id)
        {
            return db.Users.OfType<Employee>().FirstOrDefault(a => a.UserId == id);
        }

        public void Update(Employee employee)
        {
            db.Users.Update(employee);
            db.SaveChanges();
        }
    }
}
