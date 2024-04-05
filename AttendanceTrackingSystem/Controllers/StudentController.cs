using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    [Authorize]
	[Authorize(Roles = "Student")]
	public class StudentController : Controller
    {
        IRepoAttendance repoAttendance;
        IRepoStudent repoStudent;
        IRepoMsg repoMsg;
        IRepoPermission repoPermission;
        IRepoAccount repoAccount;
    
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger,IRepoAccount _repoAccount,IRepoPermission _repoPermission, IRepoAttendance _repoAttendance, IRepoStudent _repoStudent,IRepoMsg _repoMsg)
        {
            repoAttendance = _repoAttendance;
            repoStudent = _repoStudent;
            repoMsg = _repoMsg;
            repoPermission= _repoPermission; 
            repoAccount= _repoAccount;
            _logger = logger;

        }
        public IActionResult Home(int id, DateTime? endDate)
        {
            try
            {
                //            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                //            var user=repoAccount.GetUserByEmail(email);
                //            if (user != null)
                //            {
                //	id = user.UserId;
                //}
                id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var startDate = DateTime.Today.AddMonths(-1);
                endDate ??= DateTime.Today;
                var model = repoStudent.getById(id);
                if (model != null)
                {
                    ViewBag.stdDegree = model.StudentDegree;
                }

                var userAttendance = repoAttendance.GetUserAttendance(id, startDate, endDate.Value);


                int lateCount = userAttendance.Count(a => a.CheckIn > new TimeOnly(9, 0));
                int onTimeCount = userAttendance.Count(a => a.CheckIn <= new TimeOnly(9, 0));
                int absentCount = DateTime.Today.Subtract(startDate).Days - userAttendance.Count();

                List<Msg> userMessages = repoMsg.getAll(id);
                List<Permission> userPermissions=repoPermission.getAllById(id);
                var attendanceSummaryViewModel = new AttendanceSummaryViewModel
                {
                    UserId = id,
                    LateCount = lateCount,
                    AbsentCount = absentCount,
                    OnTimeCount = onTimeCount,
                    AttendanceData = userAttendance,
                    StartDate = startDate,
                    userMessages = userMessages,
                    UserPermissions = userPermissions,
                };

                var studentSchedule = repoStudent.GetFutureStudentSchedule(id);
                attendanceSummaryViewModel.StudentSchedule = studentSchedule;

                ViewBag.TrackName = repoStudent.GetTrackNameByUserId(id);
                ViewBag.Supervisor = repoStudent.GetSupervisorByStudentId(id);


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

        public IActionResult MakePermission(int userId,int SId)
        {
   
            ViewBag.userId = userId;
            TempData["SuperVisorId"] = SId;
            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult MakePermission(Permission permission)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    int SId = (TempData["SuperVisorId"] as int?) ?? -1;

                    if (SId != -1)
                    {
                      
                        var message = new Msg
                        {
                            UserId = SId,
                            Title = "Permission Request",
                            Description = "New permission request from student " + permission.UserId.ToString(),
                            Date = DateTime.Now,
                            IsRead = false
                        };

                   
                        repoPermission.Add(permission);
                        repoMsg.Add(message);
                    }
                    else
                    {
                     
                        ModelState.AddModelError("", "SupervisorId is null. Unable to send permission request.");
                        return View(permission);
                    }

                    return RedirectToAction("Home");
                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError("", "An error occurred while processing the permission request.");
                    return View(permission);
                }
            }
            else
            {
                return View(permission);
            }
        }

        [HttpPost]
        public IActionResult DeletePermission(int permissionId)
        {
            repoPermission.Delete(permissionId);
            return RedirectToAction("Home");
        }

      


        public IActionResult Index()
        {
            return View();
        }
    }
}


