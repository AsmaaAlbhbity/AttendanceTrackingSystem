using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoItiProgram
    {
        public List<ItiProgram> getAll();
        public ItiProgram getById(int id);
        public void Add(ItiProgram itiProgram);
        public void Update(ItiProgram itiProgram);
        public void Delete(int id);
    }
}
