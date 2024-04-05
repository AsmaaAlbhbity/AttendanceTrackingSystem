using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using System.Security;

namespace AttendanceTrackingSystem.Repository
{
	public class RepoSchedule : IRepoSchedule
	{
		AttendanceDBContext db;
		public RepoSchedule(AttendanceDBContext _db)
		{
			db = _db;
		}

		public void Add(Schedule schedule)
		{
			db.Schedules.Add(schedule);
			db.SaveChanges();
		}

		public void Delete(int id)
		{
			var obj = db.Schedules.FirstOrDefault(a => a.ScheduleId == id);
			db.Schedules.Remove(obj);
			db.SaveChanges();
		}

		public List<Schedule> getAll()
		{
			return db.Schedules.ToList();
		}

		public Schedule getById(int id)
		{
			return db.Schedules.FirstOrDefault(a => a.ScheduleId == id);
		}

		public void Update(Schedule schedule)
		{
			db.Schedules.Update(schedule);
			db.SaveChanges();
		}
		// Abdullah
		public List<Schedule> GetWeeklyScheduleForTrack(int trackId)
		{
			var today = DateTime.Today;
			var saturday = today;
			if (today.DayOfWeek != DayOfWeek.Saturday)
			{
				saturday = today.AddDays(-1 - (int)today.DayOfWeek);
			}
			var friday = saturday.AddDays(6);

			var weeklySchedule = db.Schedules
				.Where(s => s.TrackId == trackId && s.Date >= saturday && s.Date <= friday)
				.OrderBy(a => a.Date).ToList();
			return weeklySchedule;
		}
		public List<Schedule> GetAllScheduleForTrack(int trackId)
		{
			var today = DateTime.Today;
			var saturday = today;
			if (today.DayOfWeek != DayOfWeek.Saturday)
			{
				saturday = today.AddDays(-1 - (int)today.DayOfWeek);
			}
			var WholeSchedule = db.Schedules
				.Where(s => s.TrackId == trackId && s.Date >= saturday)
				.OrderBy(a => a.Date).ToList();
			return WholeSchedule;
		}

	}
}
