using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace AttendanceTrackingSystem.Controllers
{

    public class AttendanceViewModel
    {
        public List<Attendance> AttendanceData { get; set; }
        public int LateCount { get; set; }
        public int OnTimeCount { get; set; }
        public int AbsentCount { get; set; }
    }
    public class InstructorController : Controller
    {
  


        
        public IActionResult Index()
        {
            return View();
        }
    }
}
