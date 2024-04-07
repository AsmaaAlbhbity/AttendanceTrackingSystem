using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.ViewModel
{
	public class EditProfileViewModel
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string? ImgUrl { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Current Password")]
		[Required(ErrorMessage = "Current Password is required")]
		[Remote("CheckPassword", "Account", ErrorMessage = "Current Password is incorrect")]
		public string OldPassword { get; set; }
		[DataType(DataType.Password)]
		[Display(Name = "New Password")]
		[Required(ErrorMessage = "New Password is required")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be 8-15 characters long and contain at least one lowercase letter, one uppercase letter, one digit and one special character")]
		public string NewPassword { get; set; }
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare("NewPassword", ErrorMessage = "Password and Confirm Password do not match")]
		public string ConfirmPassword { get; set; }

	}
}
