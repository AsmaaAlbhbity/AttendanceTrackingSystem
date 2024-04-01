using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
	public interface IRepoAccount
	{
		public User GetUser(string email, string password);
		public string GetEmployeeType(int id);
	}
}
