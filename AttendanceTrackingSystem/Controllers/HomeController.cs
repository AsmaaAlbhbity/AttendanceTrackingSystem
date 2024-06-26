using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    [Authorize]
    [Authorize(Roles = "Security,StudentAffairs,Instructor,Admin,Supervisor,Student")]
    public class HomeController : Controller
    {
     
        IRepoAttendance repoAttendance;
        IRepoMsg repoMsg;
        IRepoPermission repoPermission;
        IRepoAccount repoAccount;
        IRepoStudent repoStudent;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IRepoStudent _repoStudent,IRepoAccount _repoAccount,IRepoPermission _repoPermission ,  IRepoAttendance _repoAttendance,IRepoMsg _repoMsg)
        {
            _logger = logger;
            repoAccount = _repoAccount;
           repoAttendance = _repoAttendance;
            repoMsg = _repoMsg;
            repoPermission = _repoPermission;
            repoStudent = _repoStudent;
        }
        public IActionResult Index()
        {
             return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // for security and studdent affairs and instructor
        public IActionResult Home(int userId, DateTime? endDate)
        {
            
            try
            {
		
                 userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var startDate = DateTime.Today.AddMonths(-1);
                endDate ??= DateTime.Today;
                var userAttendance = repoAttendance.GetUserAttendance(userId, startDate, endDate.Value);
                List<Msg> userMessages = repoMsg.getAll(userId);

                int lateCount = userAttendance.Count(a => a.CheckIn > new TimeOnly(9, 0));
                int onTimeCount = userAttendance.Count(a => a.CheckIn <= new TimeOnly(9, 0));
                int absentCount = DateTime.Today.Subtract(startDate).Days - userAttendance.Count();

             
                List<Permission> userPermissions = repoPermission.getAllById(userId);
                var attendanceSummaryViewModel = new AttendanceSummaryViewModel
                {
                    UserId = userId,
                    LateCount = lateCount,
                    AbsentCount = absentCount,
                    OnTimeCount = onTimeCount,
                    AttendanceData = userAttendance,
                    StartDate = startDate,
                    userMessages = userMessages,
                    UserPermissions = userPermissions,
                };
                // for studen affairs to change state
                var pendingStudents = repoStudent.GetPendingStudents();
                ViewBag.PendingStudents = pendingStudents;
                var lateOrAbsentDates = repoAttendance.GetLateOrAbsentDates(userId);
                ViewBag.LateOrAbsentDates = lateOrAbsentDates;

                return View(attendanceSummaryViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Home action");
                return View("Error");
            }
        }


        public IActionResult FetchAttendanceData(int userId, DateTime startDate)
        {
           
            var userAttendance = repoAttendance.GetUserAttendance(userId, startDate, DateTime.Today);
            return Json(userAttendance);
        }

   

        [HttpPost]
        public IActionResult MarkMessageAsRead(int userId )
        {
         
            try
            {
         
                repoMsg.MarkMessageAsRead(userId);

                return Json(true);
            }
            catch (Exception ex)
            {
              
                return Json(false);
            }
        }


    }
}
