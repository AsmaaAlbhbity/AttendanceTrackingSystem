using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoMsg : IRepoMsg
    {
        AttendanceDBContext db;
        public RepoMsg(AttendanceDBContext _db) 
        { 
            db = _db;
        }
        public void Add(Msg msg)
        {
            db.Msgs.Add(msg);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Msgs.FirstOrDefault(a => a.MsgId == id);
            db.Msgs.Remove(obj);
            db.SaveChanges();
        }

        public List<Msg> getAll()
        {
            return db.Msgs.ToList();
        }

        public Msg getById(int id)
        {
            return db.Msgs.FirstOrDefault(a => a.MsgId == id);
        }

        public void Update(Msg msg)
        {
            db.Msgs.Update(msg);
            db.SaveChanges();
        }
    }
}
