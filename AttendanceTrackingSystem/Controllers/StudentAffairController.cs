using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using AttendanceTrackingSystem.Models;
using System.Net.Mime;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;

namespace AttendanceTrackingSystem.Controllers
{
    public class StudentAffairController : Controller
    {
        int pageSize = 5;
        private readonly IRepoStudent repoStudent;
        private readonly IRepoTrack repoTrack;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public StudentAffairController(IRepoStudent _repoStudent, IRepoTrack _repoTrack, IWebHostEnvironment hostingEnvironment)
        {
            repoStudent = _repoStudent;
            repoTrack = _repoTrack;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index(string? message, int? page, string? searchTerm)
        {
            if (message != null)
            {
                TempData["SuccessMessage"] = message;
            }

            var allStudents = repoStudent.getAll(); 


            if (!string.IsNullOrEmpty(searchTerm))
            {
                allStudents = allStudents.Where(s => s.Email.Contains(searchTerm.ToLower().Trim())).ToList();
            }

            if (!page.HasValue || page < 1)
            {
                page = 1; 
            }

            int totalStudents = allStudents.Count();
            int totalPages = (int)Math.Ceiling((double)totalStudents / pageSize);

            if (page > totalPages)
            {
                page = totalPages; 
            }

            var students = allStudents.Skip((page.Value - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.SearchTerm = searchTerm; 

            return View(students);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = repoStudent.getById(id.Value);
            return PartialView("_Details", student);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {

            if (id == null)
            {
                ViewBag.Header = "Add Student";
                ViewBag.Tracks = repoTrack.getAll();
                return PartialView("_CreateOrUpdatePartial", new Student());
            }
            else
            {
                ViewBag.Header = "Update Student";
                ViewBag.Tracks = repoTrack.getAll();
                var student = repoStudent.getById(id.Value);



                // Call GetFileFromPath asynchronously to get the IFormFile object representing the image file

                return PartialView("_CreateOrUpdatePartial", student);
            }

        }


        [HttpPost]
        public IActionResult Create([Bind("UserId,Name, Email, Password, Phone, StudentDegree, StudentUniversity, StudentFaculity, StudentGraduationYear, StudentSpecialization, TrackId,Image")] Student student)
        {
            ModelState.Remove("Msgs");
            ModelState.Remove("UserType");
            ModelState.Remove("Track");
            student.UserType = "1";
            student.IsApproved = Approve.Accepted;

            if (ModelState.IsValid)
            {
                // you can search for the image in the wwrroot folders
                if (student.Image != null && student.Image.Length > 0)
                {
                    if (student.Image.ContentType == "image/png" || student.Image.ContentType == "image/jpg" || student.Image.ContentType == "image/jpeg")
                    {
                        var uniqueFileName = CreateUniqueFileName(student.Image);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/Profile", uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            student.Image.CopyTo(stream);
                        }
                        student.ImgUrl = uniqueFileName;
                        if (student.UserId != 0)
                        {
                            repoStudent.Update(student);

                            return Ok(new { message = "Student has been updated successfully." });
                        }
                        else
                        {
                            repoStudent.Add(student);
                            //return RedirectToAction("Index", new { message = "Student has been created successfully." });
                            return Ok(new { message = "Student has been created successfully." });
                        }

                        //return Ok(new { message = "Student has been created successfully." });

                    }
                    else
                    {

                        return BadRequest("Only PNG or JPEG image files are allowed.");
                    }

                }
                else
                {

                    return BadRequest("You must include a file.");
                }
            }

            return BadRequest("Incorrect data input!");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                repoStudent.Delete(id);
                return Ok(new { message = "Student has been deleted successfully." });

            }
            return BadRequest("No Student Found!");

        }

        private string CreateUniqueFileName(IFormFile file)
        {
            var uniquePart = Guid.NewGuid().ToString().Substring(0, 8); // Get the first 8 characters of the GUID
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            return $"{fileNameWithoutExtension}_{uniquePart}{extension}";
        }
        private async Task<IFormFile> GetFileFromPath(string path, string name)
        {
            string fullPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Images", "Profile", name);
            if (System.IO.File.Exists(path))
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(path);

                using (var memoryStream = new MemoryStream(fileBytes))
                {
                    return new FormFile(memoryStream, 0, fileBytes.Length, name, Path.GetFileName(path));
                }
            }
            return null;
        }



    }
}

