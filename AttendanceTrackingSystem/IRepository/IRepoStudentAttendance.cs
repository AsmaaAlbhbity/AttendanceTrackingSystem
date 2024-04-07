using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoStudentAttendance
    {
        public List<StudentAttendance> getAll();
        public StudentAttendance getById(int id);
        public void Add(StudentAttendance studentAttendance);
        public void Update(StudentAttendance studentAttendance);
        public void Delete(int id);
        public List<int> GetAttendanceForToday();
        public void DeleteByUserIdAndDate(int id);
        public void UpdateByUserIdAndDate(int id);
        public StudentAttendance GetByUserIdAndDate(int id);
        public int CheckCountOfAbsentAndLateDays(int id);
        public void UpdateDegree(int id, int newDegree);
        public bool IsLate(int id, int? scheduleId);
		public bool HavePermission(int id);
        public void SendWarningMsg(int id, int degree);
	}
}
