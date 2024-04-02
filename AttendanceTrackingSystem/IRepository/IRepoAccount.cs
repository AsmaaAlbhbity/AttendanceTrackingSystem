using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;

namespace AttendanceTrackingSystem.IRepository
{
	public interface IRepoAccount
	{
		public User GetUser(string email, string password);
		public string GetEmployeeType(int id);
		public User GetUserByid(int id);
		public void SaveEdit(EditProfileViewModel model);
		public void UpdateImage(string img, int id);

	}
}
