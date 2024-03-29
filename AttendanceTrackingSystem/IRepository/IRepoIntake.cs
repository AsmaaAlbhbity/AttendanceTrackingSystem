using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoIntake
    {
        public List<Intake> getAll();
        public Intake getById(int id);
        public void Add(Intake intake);
        public void Update(Intake intake);
        public void Delete(int id);
    }
}
