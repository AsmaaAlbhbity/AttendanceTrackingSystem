namespace AttendanceTrackingSystem.Models
{
    public enum EmployeeType
    {
        Admin,
        Security,
        StudentAffairs
    }
    public class Employee : User
    {
        public decimal EmployeeSalary { get; set; }
        public EmployeeType EmployeeType { get; set; } 
        public virtual List<Attendance> EmployeeAttendances { get; set; } = new List<Attendance>();
    }
}
