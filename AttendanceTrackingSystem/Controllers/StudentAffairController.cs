using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using AttendanceTrackingSystem.Models;
using System.Net.Mime;

namespace AttendanceTrackingSystem.Controllers
{
    public class StudentAffairController : Controller
    {
        private readonly IRepoStudent repoStudent;
        private readonly IRepoTrack repoTrack;

        public StudentAffairController(IRepoStudent _repoStudent, IRepoTrack _repoTrack)
        {
            repoStudent = _repoStudent;
            repoTrack = _repoTrack;

        }
        public IActionResult Index()
        {
            var students = repoStudent.getAll();
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
        public IActionResult Create(int? id)
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
                return PartialView("_CreateOrUpdatePartial", student);
            }

        }


        [HttpPost]
        public IActionResult Create([Bind("Name, Email, Password, Phone, StudentDegree, StudentUniversity, StudentFaculity, StudentGraduationYear, StudentSpecialization, TrackId,Image")] Student student)
        {
            ModelState.Remove("Msgs");
            ModelState.Remove("UserType");
            ModelState.Remove("Track");
            student.UserType = "Student";
            student.IsApproved = Approve.Accepted;

            if (ModelState.IsValid)
            {
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
                        repoStudent.Add(student);
                        return Ok(new { message = "Student has been created successfully." });

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

        private string CreateUniqueFileName(IFormFile file)
        {
            var uniquePart = Guid.NewGuid().ToString().Substring(0, 8); // Get the first 8 characters of the GUID
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            return $"{fileNameWithoutExtension}_{uniquePart}{extension}";
        }


    }
}

