using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
     
        IRepoAttendance repoAttendance;
        IRepoMsg repoMsg;
        IRepoPermission repoPermission;
        IRepoAccount repoAccount;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IRepoAccount _repoAccount,IRepoPermission _repoPermission ,  IRepoAttendance _repoAttendance,IRepoMsg _repoMsg)
        {
            _logger = logger;
            repoAccount = _repoAccount;
           repoAttendance = _repoAttendance;
            repoMsg = _repoMsg;
            repoPermission = _repoPermission;
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
				var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
				var user = repoAccount.GetUserByEmail(email);
				if (user != null)
				{
					userId = user.UserId;
				}
				
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
