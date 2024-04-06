using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Extensions
{
    public class GraduationYearAttribute : ValidationAttribute
    {
        int length;
        public GraduationYearAttribute(int _length) 
        {
            length = _length;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime graduationYear)
            {
                int currentYear = DateTime.Now.Year;

                // Calculate the minimum allowed graduation year based on the current year and the specified length
                int minGraduationYear = currentYear - length;

                if (graduationYear.Year >= minGraduationYear && graduationYear.Year <= currentYear)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Graduation year must be between {minGraduationYear} and {currentYear}.");
                }
            }

            return new ValidationResult("Invalid graduation year.");
        }
    }
}
