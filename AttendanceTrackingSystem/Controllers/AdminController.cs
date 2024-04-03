using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class AdminController : Controller
    {
        IRepoUser repoUser;
        IRepoAttendance repoAttendance;
        IRepoEmployee repoEmployee;
        IRepoTrack repoTrack;

        public AdminController(IRepoTrack _repoTrack,IRepoAttendance _repoAttendance,IRepoUser _repoUser,IRepoEmployee _repoEmployee)
        {

           repoAttendance = _repoAttendance;
            repoUser=_repoUser;
            repoEmployee= _repoEmployee;
            repoTrack= _repoTrack;
           

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {

            ViewBag.stdAffCount = repoEmployee.getEmpCount(Models.EmployeeType.StudentAffairs);
            ViewBag.secCount = repoEmployee.getEmpCount(Models.EmployeeType.Security);
            ViewBag.trackCount = repoTrack.getAll().Count();

            var userTypeCounts = repoUser.GetUserTypeCounts();
            var attendanceCountsPerUserType = repoAttendance.GetAttendanceCountsPerUserType();

            var labels = attendanceCountsPerUserType.Select(x => x.UserType).ToArray();
            var data = attendanceCountsPerUserType.Select(x => x.AttendanceCount).ToArray();

           
            var model = new AdminHomeViewModel
            {
                UserTypeCounts = userTypeCounts,
                AttendanceLabels = labels,
                AttendanceData = data
            };

            return View(model);
        }
    }
}
