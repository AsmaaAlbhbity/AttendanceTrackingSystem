﻿using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using AttendanceTrackingSystem.Models;
using System.Net.Mime;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using OfficeOpenXml;

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
        [HttpPost]
        public IActionResult UploadStudents(IFormFile excelFile)
        {
            int studentAdded = 0;
            try
            {
                if (excelFile == null || excelFile.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }
                else if (excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    return BadRequest("Only Excel files are allowed.");
                }

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                     
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (String.IsNullOrEmpty( worksheet.Cells[row, 1].Value?.ToString()))
                            {
                                break;
                            }
                           
                            string name = worksheet.Cells[row, 1].Value?.ToString();
                            string email = worksheet.Cells[row, 2].Value?.ToString();
                            string phone = worksheet.Cells[row, 3].Value?.ToString();
                            string password = worksheet.Cells[row, 4].Value?.ToString();
                            int isApproved = int.Parse(worksheet.Cells[row, 5].Value?.ToString());
                            int userType = int.Parse(worksheet.Cells[row, 6].Value?.ToString());
                            string imgUrl = worksheet.Cells[row, 7].Value?.ToString();
                            int studentDegree = int.Parse(worksheet.Cells[row, 8].Value?.ToString());
                            string studentUniversity = worksheet.Cells[row, 9].Value?.ToString();
                            string studentFaculity = worksheet.Cells[row, 10].Value?.ToString();
                            int _studentGraduationYear = int.Parse(worksheet.Cells[row, 11].Value?.ToString());
                            DateTime studentGraduationYear = new DateTime(_studentGraduationYear, 1, 1);
                            //DateTime studentGraduationYear = Convert.ToDateTime( worksheet.Cells[row, 11].Value?.ToString());
                            string studentSpecialization = worksheet.Cells[row, 12].Value?.ToString();
                            int trackId = int.Parse(worksheet.Cells[row, 13].Value?.ToString());

                            Student s = new Student
                            {
                                Name = name,
                                Email = email,
                                Phone = phone,
                                Password = password,
                                IsApproved = (Approve)isApproved,
                                UserType = userType == 1 ? "1" : "2",
                                ImgUrl = imgUrl,
                                StudentDegree = studentDegree,
                                StudentUniversity = studentUniversity,
                                StudentFaculity = studentFaculity,
                                StudentGraduationYear = studentGraduationYear,
                                StudentSpecialization = studentSpecialization,
                                TrackId = trackId
                            };

                            repoStudent.Add(s);
                            studentAdded++;
                        }
                    }
                }

                return Ok($"Students uploaded successfully.\n number of Students Added: {studentAdded}");
            }
            catch (Exception ex)
            {

                return BadRequest($"An error occurred while processing the file. Please try again later. \n number of Students Added: {studentAdded}");
            }
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
