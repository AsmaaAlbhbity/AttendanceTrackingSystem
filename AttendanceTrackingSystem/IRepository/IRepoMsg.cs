using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoMsg
    {
        public List<Msg> getAll();
        public Msg getById(int id);
        public void Add(Msg msg);
        public void Update(Msg msg);
        public void Delete(int id);
        public void MarkMessageAsRead(int userId);
        public List<Msg> getAll(int userId);
    }
}
