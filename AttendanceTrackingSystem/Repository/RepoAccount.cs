using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
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
	}
}
