using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoEmployee
    {
        public List<Employee> getAll();
        public Employee getById(int id);
        public void Add(Employee employee);
        public void Update(Employee employee);
        public void Delete(int id);

        public int getEmpCount(EmployeeType employeeType);
    }
}
