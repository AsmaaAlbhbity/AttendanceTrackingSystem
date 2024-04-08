using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

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
            Console.WriteLine($"the id is {id} the ob is {obj}");
            if (obj != null)
            {
                db.Employees.Remove(obj);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("the obj is null ");
            }
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
        public Employee GetByEmail(string email)
        {
            return db.Employees.FirstOrDefault(e => e.Email == email);
        }
    }
}
