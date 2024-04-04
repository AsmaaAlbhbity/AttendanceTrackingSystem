using AttendanceTrackingSystem.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepoStudent repoStudent;

        public EmployeeController(IRepoStudent _repoStudent) 
        {
            repoStudent = _repoStudent;
        }
        public IActionResult Index()
        {
            var students = repoStudent.getAll();
            return View(students);
        }

    }
}
