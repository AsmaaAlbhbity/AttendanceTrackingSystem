﻿using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{

    public class AttendanceViewModel
    {
        public List<AttendanceController> AttendanceData { get; set; }
        public int LateCount { get; set; }
        public int OnTimeCount { get; set; }
        public int AbsentCount { get; set; }
    }

    [Authorize(Roles = "Supervisor")]
    public class InstructorController : Controller
    {
		private int supervisorId;
		int pageSize = 5;

        private readonly IRepoPermission repoPermission;
        private readonly IRepoStudent repoStudent;
        public InstructorController(IRepoPermission _repoPermission, IRepoStudent _repoStudent)
        {
            repoPermission = _repoPermission;
            repoStudent = _repoStudent;
         
		}
  
        public IActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles = "Supervisor")]
        //[Authorize(Roles = "Instructor,Supervisor")]
        [HttpGet]
        public IActionResult ViewStudentPermissions(int? page)
        {
			supervisorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

			if (!page.HasValue || page < 1)
            {
                page = 1;
            }

         
            var permissions = repoPermission.GetAllBySupervisorId(supervisorId).OrderByDescending(c=>c.Date);
            int totalPremissions = permissions.Count();
            int totalPages = (int)Math.Ceiling((double)totalPremissions / pageSize);

            if (page > totalPages)
            {
                page = totalPages;
            }
            var _permissions = permissions.Skip((page.Value - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            return View(_permissions);
        }
        
        public IActionResult AcceptPermission(int id)
        {
            var permission = repoPermission.getById(id);
            permission.State = PermissionState.Approved;
            try
            {
                repoPermission.Update(permission);
                TempData["SuccessMessage"] = "Permission Accepted!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                throw;
            }
 
            return RedirectToAction("ViewStudentPermissions");
        }
        public IActionResult RefusePermission(int id) {             
            var permission = repoPermission.getById(id);
            permission.State = PermissionState.Rejected;
            try
            {
                repoPermission.Update(permission);
                TempData["SuccessMessage"] = "Permission Refused!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                throw;
            }
            return RedirectToAction("ViewStudentPermissions");
        }
        public IActionResult Details(int id)
        {
            var permission = repoPermission.getById(id);
            return PartialView("_Details",permission);
        }

    }
}
