using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoPermission : IRepoPermission
    {
        AttendanceDBContext db;
        public RepoPermission(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Permission permission)
        {
            db.Permissions.Add(permission);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Permissions.FirstOrDefault(a => a.PermissionId == id);
            db.Permissions.Remove(obj);
            db.SaveChanges();
        }

        public List<Permission> getAll()
        {
            return db.Permissions.ToList();
        }

        public List<Permission> getAllById(int userId)
        {
            return db.Permissions.Where(p=>p.UserId==userId).ToList();
        }
        public Permission getById(int id)
        {
            return db.Permissions.FirstOrDefault(a => a.PermissionId == id);
        }

        public void Update(Permission permission)
        {
            db.Permissions.Update(permission);
            db.SaveChanges();
        }
    }
}
