using AttendanceTrackingSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.ViewModel
{
    public class EmployeeViewModel
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"^([\w-\.]{3,20})+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must start with 010, 011, 012, or 015 and be followed by 8 digits.")]
        public string Phone { get; set; }


        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$%])[A-Za-z\d@$%_]{8,}$", ErrorMessage = "Must be 8 characters or more.\r\nMust contain at least one uppercase letter.\r\nMust contain at least one lowercase letter.\r\nMust contain at least one digit.\r\nMust contain at least one special character from @$%_")]
        public string Password { get; set; }

        public string? ImgUrl { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal EmployeeSalary { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public string UserType { get; set; }

        public Approve IsApproved { get; set; }
    }
}
