using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class Attendance : Controller
    {
        public IActionResult AttendanceStudent()
        {
            return View();
        }
    }
}
