using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Repository
{
	public class RepoAccount : IRepoAccount
	{
		AttendanceDBContext db;
		public RepoAccount(AttendanceDBContext _db)
		{
			db = _db;
		}
		public User GetUser(string email, string password)
		{
			return db.Users.FirstOrDefault(a => a.Email == email && a.Password == password);
		}
		public string GetEmployeeType(int id)
		{
			return db.Employees.FirstOrDefault(a => a.UserId == id).EmployeeType.ToString();
		}
<<<<<<< HEAD

		public User GetUserByEmail(string email)
		{
			return db.Users.FirstOrDefault(u => u.Email == email);
		}
=======
		public User GetUserByid(int id)
		{
			return db.Users.FirstOrDefault(a => a.UserId == id);
		}
		public void SaveEdit(EditProfileViewModel model)
		{
			var user = GetUser(model.Email, model.OldPassword);
			if (user == null)
				return;
			if (model.NewPassword != null)
				user.Password = model.NewPassword;
			db.SaveChanges();
		}
		public void UpdateImage(string img, int id)
		{
			var user = GetUserByid(id);
			if (user == null)
				return;
			user.ImgUrl = img;
			db.SaveChanges();
		}


>>>>>>> main
	}
}
