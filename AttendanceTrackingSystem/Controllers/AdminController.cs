using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Pagination;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Drawing.Printing;

namespace AttendanceTrackingSystem.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        IRepoInstructor repoInstructor;
        IRepoTrack repoTrack;
        IRepoUser repoUser;
        IRepoEmployee repoEmployee;
        IRepoAttendance repoAttendance;
		IRepoStudentAttendance s;
        public AdminController(IRepoInstructor _repoInstructor, IRepoTrack _repoTrack,IRepoUser _repoUser,IRepoEmployee _repoEmployee,IRepoAttendance _repoAttendance , IRepoStudentAttendance _s)
        {
            repoInstructor = _repoInstructor;
            repoTrack = _repoTrack;
            repoUser = _repoUser;
             repoEmployee= _repoEmployee;
            repoAttendance = _repoAttendance;
            s = _s;
        }



        


        public IActionResult ShowInstructor(int pageNumber = 1, int pageSize = 4)
        {

		var allInstructors = repoInstructor.getAll().AsQueryable(); // Ensure IQueryable

            // Count total records
            var totalRecords = allInstructors.Count();

            // Paginate the instructors
            var paginatedInstructors = PaginatedList<Instructor>.Create(allInstructors, pageNumber, pageSize);

            // Retrieve the instructors for the current page
            var instructorsForPage = paginatedInstructors.ToList();

            // Retrieve all tracks
            var tracks = repoTrack.getAll();

            // Create the view model
            var model = new ShowInstructorViewModel
            {
                Instroctors = instructorsForPage,
                Tracks = tracks,
                InstructorsPaination = new PaginatedList<Instructor>(instructorsForPage, totalRecords, pageNumber, pageSize)
            };

            return View(model);
        }

        public IActionResult AddInstructor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddInstructor(Instructor instructor , IFormFile ImgUrl)
        {
            instructor.UserType = "Instructor";

            try
            {
                if (ModelState.IsValid)
                {
                    if (ImgUrl != null)
                    {
                        string ImgeName = instructor.UserId + instructor.Name + "." + ImgUrl.FileName.Split(".").Last();

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
        public async Task<IActionResult> EditInstructor(int id, Instructor instructor, IFormFile? ImgUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingInstructor = repoInstructor.getById(id);
                    if (existingInstructor == null)
                    {
                        return NotFound(); // Handle not found case
                    }

                    // Update instructor properties
                    existingInstructor.Name = instructor.Name;
                    existingInstructor.Email = instructor.Email;
                    existingInstructor.Password = instructor.Password;
                    existingInstructor.Phone = instructor.Phone;
                    existingInstructor.InstructorSalary = instructor.InstructorSalary;

                    if (ImgUrl != null)
                    {
                        string ImgeName = existingInstructor.UserId + existingInstructor.Name + "." + ImgUrl.FileName.Split(".").Last();

                        using (var fs = new FileStream("wwwroot/Images/" + ImgeName, FileMode.Create))
                        {
                            await ImgUrl.CopyToAsync(fs);
                        }

                        existingInstructor.ImgUrl = ImgeName;
                    }
                    repoInstructor.Update(existingInstructor);
                    return RedirectToAction("ShowInstructor");
                }

                return View(instructor);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    ModelState.AddModelError("Email", "The email address is already in use.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating the instructor.");
                }

                return View(instructor);
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
            ViewBag.tracks=repoTrack.getAll();

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
        public IActionResult Employee(string searchString, int pageNumber = 1, int pageSize = 4)
    {
        var employees = repoEmployee.getAll();

       if (!string.IsNullOrEmpty(searchString))
{
    employees = employees.Where(e => e.Name.Contains(searchString) || e.Email.Contains(searchString)).ToList();
}



        var employeeUsers = employees.Where(e => e.UserType == "Employee").AsQueryable();
        var model = PaginatedList<Employee>.Create(employeeUsers, pageNumber, pageSize);

        return View(model);
    }







    // GET: Admin/Details/5
    public ActionResult Details(int id)
    {
        var employee = repoEmployee.getById(id);
        if (employee == null)
        {
            return NotFound();
        }

        var viewModel = new EmployeeViewModel
        {
            UserId = employee.UserId,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Password = employee.Password,
            ImgUrl = employee.ImgUrl,
            EmployeeSalary = employee.EmployeeSalary,
            EmployeeType = employee.EmployeeType,
            UserType = employee.UserType,
            IsApproved = employee.IsApproved
        };

        return View(viewModel);
    }

    // GET: Admin/Create
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(EmployeeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Check if the email already exists
            var existingEmployee = _repoEmployee.GetByEmail(viewModel.Email);
            if (existingEmployee != null)
            {
                ModelState.AddModelError("Email", "Email address already exists.");
                return View(viewModel);
            }

            // Process the uploaded file if one exists
            if (viewModel.Photo != null && viewModel.Photo.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await viewModel.Photo.CopyToAsync(stream);
                    viewModel.ImgUrl = $"data:{viewModel.Photo.ContentType};base64,{Convert.ToBase64String(stream.ToArray())}";
                }
            }

            var employee = new Employee
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Password = viewModel.Password,
                ImgUrl = viewModel.ImgUrl,
                EmployeeSalary = viewModel.EmployeeSalary,
                EmployeeType = viewModel.EmployeeType,
                UserType = "Employee",
                IsApproved = Approve.Accepted
            };

            try
            {
                repoEmployee.Add(employee);
                return RedirectToAction("Employee");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unable to save changes due to an error: {ex.Message}");
            }
        }

        return View(viewModel);
    }











    // GET: Admin/Edit/5
    public ActionResult Edit(int id)
    {
        var employee = repoEmployee.getById(id);
        if (employee == null)
        {
            return NotFound();
        }

        var viewModel = new EmployeeViewModel
        {
            UserId = employee.UserId,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Password = employee.Password,
            ImgUrl = employee.ImgUrl,
            EmployeeSalary = employee.EmployeeSalary,
            EmployeeType = employee.EmployeeType,
            UserType = "Employee",
            IsApproved = Approve.Accepted
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(EmployeeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var existingEmployeeWithEmail = _repoEmployee.GetByEmail(viewModel.Email);
            if (existingEmployeeWithEmail != null && existingEmployeeWithEmail.UserId != viewModel.UserId)
            {
                ModelState.AddModelError("Email", "Email address already exists.");
                return View(viewModel);
            }

            var employee = _repoEmployee.getById(viewModel.UserId);
            if (employee == null)
            {
                return NotFound();
            }

            // Process the uploaded file if one exists
            if (viewModel.Photo != null && viewModel.Photo.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await viewModel.Photo.CopyToAsync(stream);
                    viewModel.ImgUrl = $"data:{viewModel.Photo.ContentType};base64,{Convert.ToBase64String(stream.ToArray())}";
                }
            }

            employee.Name = viewModel.Name;
            employee.Email = viewModel.Email;
            employee.Phone = viewModel.Phone;
            employee.Password = viewModel.Password;
            employee.ImgUrl = viewModel.ImgUrl;
            employee.EmployeeSalary = viewModel.EmployeeSalary;
            employee.EmployeeType = viewModel.EmployeeType;
            employee.UserType = "Employee";
            employee.IsApproved = Approve.Accepted;

            try
            {
                repoEmployee.Update(employee);
                return RedirectToAction("Employee");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }

        return View(viewModel);
    }





    // GET: Admin/Delete/5
    public ActionResult Delete(int id)
    {
        var employee = repoEmployee.getById(id);
        if (employee == null)
        {
            return NotFound();
        }

        var viewModel = new EmployeeViewModel
        {
            UserId = employee.UserId,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Password = employee.Password,
            ImgUrl = employee.ImgUrl,
            EmployeeSalary = employee.EmployeeSalary,
            EmployeeType = employee.EmployeeType,
            UserType = employee.UserType,
            IsApproved = employee.IsApproved
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
       
        var employeeToDelete = _repoEmployee.getById(id);
        if (employeeToDelete == null)
        {
            return NotFound();
        }

        try
        {
            _repoEmployee.Delete(id); 
            return RedirectToAction(nameof(Employee));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Unable to delete employee due to an error: {ex.Message}");
            return RedirectToAction(nameof(Employee));         }
    }
    

    public IActionResult Tracks(int pageNumber = 1, int pageSize = 4)
        {
            var allTracks = repoTrack.getAll().AsQueryable(); // Ensure IQueryable

            // Count total records
            var totalRecords = allTracks.Count();

            // Paginate the instructors
            var paginatedTracks = PaginatedList<Track>.Create(allTracks, pageNumber, pageSize);

            // Retrieve the instructors for the current page
            var instructorsForPage = paginatedTracks.ToList();

            
            var AllTracks = repoTrack.getAll();
            var AllInstructor = repoInstructor.getAll().Where(instructor => !repoTrack.getAll().Any(track => track.Instructor.UserId == instructor.UserId)).ToList();

            TrackViewModel model = new TrackViewModel()
            {
                Tracks = AllTracks,
                Instructors = AllInstructor,
                Supervisour = repoInstructor.getAll(),
                TracksPaination = new PaginatedList<Track>(instructorsForPage, totalRecords, pageNumber, pageSize)
            };
            return View(model);
        }
        public IActionResult ChangeSupervisourForTrack(int id , int Trackid)
        {
            var track = repoTrack.getById(Trackid);
            track.SupervisorId = id;
            repoTrack.Update(track);

            return Ok();
        }
        public IActionResult ChangeStateForTrack(int id)
        {
            var track = repoTrack.getById(id);
            track.IsActive = !track.IsActive;
            repoTrack.Update(track);

            return Ok();
        }

    }
}






