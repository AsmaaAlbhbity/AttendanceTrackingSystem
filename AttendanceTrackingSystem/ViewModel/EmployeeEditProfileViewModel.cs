using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModel
{
	public class EmployeeEditProfileViewModel : EditProfileViewModel
	{
		public decimal EmployeeSalary { get; set; }
		public EmployeeType EmployeeType { get; set; }
	}
}
