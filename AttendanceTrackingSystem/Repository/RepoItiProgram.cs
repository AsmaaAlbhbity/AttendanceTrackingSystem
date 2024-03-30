using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoItiProgram : IRepoItiProgram
    {
        AttendanceDBContext db;
        public RepoItiProgram(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(ItiProgram itiProgram)
        {
            db.ItiProgram.Add(itiProgram);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.ItiProgram.FirstOrDefault(a => a.ItiProgramId == id);
            db.ItiProgram.Remove(obj);
            db.SaveChanges();
        }

        public List<ItiProgram> getAll()
        {
            return db.ItiProgram.ToList();
        }

        public ItiProgram getById(int id)
        {
            return db.ItiProgram.FirstOrDefault(a => a.ItiProgramId == id);
        }

        public void Update(ItiProgram itiProgram)
        {
            db.ItiProgram.Update(itiProgram);
            db.SaveChanges();
        }
    }
}
