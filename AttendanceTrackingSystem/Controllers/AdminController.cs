using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Controllers
{
    public class AdminController : Controller
    {
        IRepoInstructor repoInstructor;
        IRepoTrack repoTrack;
        IRepoUser repoUser;
        IRepoEmployee repoEmployee;
        IRepoAttendance repoAttendance;
        public AdminController(IRepoInstructor _repoInstructor, IRepoTrack _repoTrack,IRepoUser _repoUser,IRepoEmployee _repoEmployee,IRepoAttendance _repoAttendance)
        {
            repoInstructor = _repoInstructor;
            repoTrack = _repoTrack;
            repoUser = _repoUser;
             repoEmployee= _repoEmployee;
            repoAttendance = _repoAttendance;
        }
        public IActionResult ShowInstructor()
        {
            var Instructor = repoInstructor.getAll();
            var Tracks = repoTrack.getAll();

            ShowInstructorViewModel model = new ShowInstructorViewModel();
            model.Instroctors = Instructor;
            model.Tracks = Tracks;

            return View(model);
        }
        public IActionResult AddInstructor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddInstructor(Instructor instructor , IFormFile ImgUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ImgUrl != null)
                    {
                        string ImgeName = instructor.Name + instructor.Name + "." + ImgUrl.FileName.Split(".").Last();

                        using (var fs = new FileStream("wwwroot/Images/" + ImgeName, FileMode.Create))
                        {
                            await ImgUrl.CopyToAsync(fs);
                        }

                        instructor.ImgUrl = ImgeName;

                    }
                    repoInstructor.Add(instructor);
                    return RedirectToAction("ShowInstructor");
                }
                return View(instructor);

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    ModelState.AddModelError("Email", "The email address is already in use.");
                    return View(instructor); 
                }
                else
                {
                    throw;
                }
            }
        }
        public IActionResult EditInstructor(int id )
        {
            if (id == null)
                return BadRequest();

            var model = repoInstructor.getById(id);
            if (model == null)
                return NotFound();
           
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditInstructor(int id , Instructor instructor , IFormFile ImgUrl)
        {
            instructor.UserId = id;
            try
            {
                if (ModelState.IsValid)
                {
                    if (ImgUrl != null)
                    {
                        string ImgeName = instructor.Name + instructor.Name + "." + ImgUrl.FileName.Split(".").Last();

                        using (var fs = new FileStream("wwwroot/Images/" + ImgeName, FileMode.Create))
                        {
                            await ImgUrl.CopyToAsync(fs);
                        }

                        instructor.ImgUrl = ImgeName;

                    }
                    repoInstructor.Update(instructor);
                    return RedirectToAction("ShowInstructor");
                }
                return View(instructor);

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    ModelState.AddModelError("Email", "The email address is already in use.");
                    return View(instructor);
                }
                else
                {
                    throw;
                }
            }
        }
        public IActionResult ChooseSupervisor(int userId)
        {
            var track = repoTrack.getAll().FirstOrDefault(a=>a.SupervisorId== userId);
            var instructors = repoInstructor.getAll().Where(instructor => !repoTrack.getAll().Any(track => track.Instructor.UserId == instructor.UserId)).ToList();

            ChooseSupervisorViewModel model = new ChooseSupervisorViewModel();
            model.Track = track;
            model.Instructors = instructors;
            model.UserId = userId;

            return View(model);
        }
        [HttpPost]
        public IActionResult ChooseSupervisor(int userId, Track track)
        {
            var obj = repoTrack.getById(track.TrackId);
            obj.SupervisorId = track.SupervisorId;
            repoTrack.Update(obj);


            var instructor = repoInstructor.getById(userId);
            if (instructor != null)
            {
                foreach (var t in instructor.Tracks.ToList())
                {
                    instructor.Tracks.Remove(t);
                }
                repoInstructor.Delete(userId);
            }

            return RedirectToAction("ShowInstructor");
        }


        public IActionResult ManageTrack(int id)
        {
            var instructor = repoInstructor.getById(id);

            var insTrack = repoTrack.getAll()
                .Where(a => a.IsActive && a.Instructors.Any(instructor => instructor.UserId == id))
                .ToList();

            var Tracks = repoTrack.getAll().Where(a => a.IsActive = true).ToList();
            var insNoTrack = Tracks.Except(insTrack).ToList();

           ManageTrackViewModel model = new ManageTrackViewModel();
            model.Instructor = instructor;
            model.InstructorTrack = insTrack;
            model.AnotherTrack = insNoTrack;



            return View(model);
        }
        [HttpPost]
        public IActionResult ManageTrack(List<int> TrackToAdd, List<int> TrackToRemove, int id)
        {
            var instructor = repoInstructor.getById(id);

            if (instructor != null)
            {
                foreach (var item in TrackToAdd)
                { 
                    repoInstructor.AddTrack(item, id);
                }

                foreach (var item in TrackToRemove)
                {
                    repoInstructor.RemoveTrack(item, id);
                }
            }

            return RedirectToAction("ShowInstructor");
        }
        public IActionResult DeleteInstructor(int userId)
        {
            if (repoTrack.getAll().Any(a => a.SupervisorId == userId))
            {
                return Json(new { success = false, message = "Cannot delete instructor. Please choose a new supervisor." });
            }

            var instructor = repoInstructor.getById(userId);
            if (instructor != null)
            {
                foreach (var t in instructor.Tracks.ToList())
                {
                    instructor.Tracks.Remove(t);
                }
                repoInstructor.Delete(userId);
            }

            return Json(new { success = true, redirectUrl = Url.Action("ShowInstructor") });
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
