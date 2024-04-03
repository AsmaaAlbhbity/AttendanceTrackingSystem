using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoEmployee : IRepoEmployee
    {
        AttendanceDBContext db;
        public RepoEmployee(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Employees.FirstOrDefault(a => a.UserId == id);
            db.Employees.Remove(obj);
            db.SaveChanges();
        }

        public List<Employee> getAll()
        {
            return db.Employees.ToList();
        }

        public Employee getById(int id)
        {
            return db.Employees.FirstOrDefault(a => a.UserId == id);
        }

        public void Update(Employee employee)
        {
           db.Employees.Update(employee);
            db.SaveChanges();
        }


        // asmaa
        public int getEmpCount(EmployeeType employeeType)
        {
            int employeeCount = db.Users.OfType<Employee>()
                                      .Count(e => e.EmployeeType == employeeType);
            return employeeCount;

        }
    }
}
