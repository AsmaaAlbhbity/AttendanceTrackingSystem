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
        public IActionResult Create()
        {
            ViewBag.Tracks = repoTrack.getAll();
            return PartialView("_CreateOrUpdatePartial", new Student());
        }


        [HttpPost]
        public IActionResult Create([Bind("Name, Email, Password, Phone, StudentDegree, StudentUniversity, StudentFaculity, StudentGraduationYear, StudentSpecialization, TrackId")] Student student, IFormFile file)
        {
            ModelState.Remove("Msgs");
            ModelState.Remove("UserType");
            ModelState.Remove("Track");
            student.UserType = "Student";
            student.IsApproved = Approve.Accepted;

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    if (/*file.ContentType == "image/png" ||*/ file.ContentType == "image/jpg" || file.ContentType == "image/jpeg")
                    {
                        var uniqueFileName = CreateUniqueFileName(file);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/Profile", uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        student.ImgUrl = uniqueFileName;

                        //repoStudent.Add(student);
                        //Preivew Toast Sucess
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ModelState.AddModelError("file", "Only PNG or JPEG or JPG image files are allowed.");
                        ModelState.AddModelError("", "Only PNG or JPEG or JPG image files are allowed.");
                        ModelState.AddModelError("fileInput", "Only PNG or JPEG or JPG image files are allowed.");
                        return PartialView("_CreateOrUpdatePartial", student);

                    }

                }
                else
                {
                    ModelState.AddModelError("file", "Please Select An Image To Upload");
                    return PartialView("_CreateOrUpdatePartial", student);
                }


            }

            return PartialView("_CreateOrUpdatePartial", student);
        }

        private string CreateUniqueFileName(IFormFile file)
        {
            return String.Concat(file.FileName.Split('.').First(), new Guid(),'.', file.FileName.Split('.').Last());

        }


    }
}

