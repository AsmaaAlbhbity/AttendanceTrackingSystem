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
		public int CheckStudentStatus(TimeOnly arriveTime, TimeOnly timeInScedule,int id);
		public void CreateInstructorAndEmployeeAttendanceRecords(DateTime date);
		public void CreateStudentAttendanceRecords(int scheduleId, DateTime date);
		public void CreateAttendanceRecords(int scheduleId, DateTime date);
		public bool checkSechduleToday();
		public bool checkSechduleTodayFoeTrack(int id);


    }
}
