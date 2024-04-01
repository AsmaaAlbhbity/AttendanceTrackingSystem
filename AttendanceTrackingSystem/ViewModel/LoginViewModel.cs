using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.ViewModel
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Please enter your email")]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Please enter your password")]
		public string Password { get; set; }
	}
}
