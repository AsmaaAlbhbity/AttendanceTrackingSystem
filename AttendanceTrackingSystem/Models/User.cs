﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AttendanceTrackingSystem.Models
{
    public enum Approve
    {
        Accepted,
       
        pending,
        Fired,
        Rejected

    }
    public enum UserType
    {
        Student,
        Instructor,
        Admin,
        Employee,
    }

    public class User
    {
        public int UserId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters long.")]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [MaxLength(11, ErrorMessage = "Phone number must not exceed 11 characters.")]
        [RegularExpression(@"^(010|011|015)\d{8}$", ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Approve IsApproved { get; set; }
        public string UserType { get; set; } 
        public string? ImgUrl { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        [JsonIgnore]
        public virtual List<Msg>? Msgs { get; set; }
    }
}
