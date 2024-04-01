using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoUser
    {
        public List<User> getAll();
        public User getById(int id);
        public void Add(User user);
        public void Update(User user);
        public void Delete(int id);
        public bool EmailIsUnique (string email);   
    }
}
