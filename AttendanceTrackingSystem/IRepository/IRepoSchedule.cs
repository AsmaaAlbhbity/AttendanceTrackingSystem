using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoSchedule
    {
        public List<Schedule> getAll();
        public Schedule getById(int id);
        public void Add(Schedule schedule);
        public void Update(Schedule schedule);
        public void Delete(int id);
        public List<Schedule> GetWeeklyScheduleForTrack(int TrackId);
        public List<Schedule> GetAllScheduleForTrack(int trackId);
        public List<Schedule> CreateNextWeekSchedukeTemplate();
        public bool IsScheduleExist(int trackId, DateTime date);
        public void DeleteByIdAndDate(int trackId, DateTime date);
        public void AddOrReplaceSchedule(Schedule schedule);


    }
}
