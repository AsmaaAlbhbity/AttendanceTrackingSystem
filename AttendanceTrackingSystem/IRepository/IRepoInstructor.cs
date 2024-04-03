using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoInstructor
    {
        public List<Instructor> getAll();
        public Instructor getById(int id);
        public void Add(Instructor instructor);
        public void Update(Instructor instructor);
        public void Delete(int id);
    

    }
}
