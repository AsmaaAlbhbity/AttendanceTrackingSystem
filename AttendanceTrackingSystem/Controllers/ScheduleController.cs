﻿using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.DependencyResolver;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class ScheduleController : Controller
    {
        IRepoInstructor repoInstructor;
        IRepoSchedule repoSchedule;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(ILogger<ScheduleController> logger, IRepoSchedule _repoSchedule, IRepoInstructor _repoInstructor)
        {
            repoSchedule = _repoSchedule;
            repoInstructor = _repoInstructor;

            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Instructor,Supervisor")]
        [HttpPost]
        public IActionResult ProcessTrackSchedule(int? trackId)
        {
            if (trackId == null)
            {
                // Handle the situation when no track ID is provided
                return RedirectToAction("Error", "Home");
            }

            // Redirect to the same action but using the GET method
            return RedirectToAction("ShowTrackSchedule", new { trackId = trackId });
        }
        [Authorize(Roles = "Instructor,Supervisor")]
        [HttpGet]
        public IActionResult ShowTrackSchedule(int? trackId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                List<Track> InstTracks = repoInstructor.GetInstructorTracks(userId);
                if (trackId == null)
                {
                    if (InstTracks.Any())
                    {
                        trackId = InstTracks.First().TrackId;
                    }
                    else
                    {
                        return View("Error", "Home");
                    }
                }

                ViewBag.InstructorTracks = InstTracks;
                ViewBag.SelectedTrackId = trackId;
                ViewBag.IsSupervisor = repoInstructor.IsSuperisor(userId);
                if (InstTracks.Any(t => t.TrackId == trackId))
                {
                    List<Schedule> WeekSchedule = repoSchedule.GetWeeklyScheduleForTrack(trackId.Value);
                    ViewBag.SelectedTrackName = InstTracks.FirstOrDefault(a => a.TrackId == trackId).Name;
                    return View(WeekSchedule);
                }
                else
                {
                    return View("Error", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in ShowTrackSchedule action");
                return View("Error");
            }
        }

        public IActionResult ViewAllSchedule()
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                int trackId = repoInstructor.GetTrackBySupervisor(userId);
                List<Schedule> WholeSchedule = repoSchedule.GetAllScheduleForTrack(trackId);
                return View(WholeSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in ViewAllSchedule action");
                return View("Error");
            }
        }
        public IActionResult AddSchedule()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            int trackId = repoInstructor.GetTrackBySupervisor(userId);
            ViewBag.TrackId = trackId;
            var upcomingWeekSchedules = repoSchedule.CreateNextWeekScheduleTemplate();
            return View(upcomingWeekSchedules);
        }

        [HttpPost]
        public IActionResult AddSchedule(List<Schedule> schedules)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                int trackId = repoInstructor.GetTrackBySupervisor(userId);

                foreach (var schedule in schedules)
                {
                    repoSchedule.AddOrReplaceSchedule(schedule);
                }

                var WholeSchedule = repoSchedule.GetAllScheduleForTrack(trackId); // Retrieve the updated schedule list
                return View("ViewAllSchedule", WholeSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddSchedule action");
                return View("Error");
            }
        }
        public IActionResult EditSchedule()
        {
            try
            {
				int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				int trackId = repoInstructor.GetTrackBySupervisor(userId);
				ViewBag.TrackId = trackId;
                List<Schedule> WholeSchedule = repoSchedule.GetAllScheduleForTrack(trackId);
                return View("AddSchedule", WholeSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in EditSchedule action");
                return View("Error");
            }

        }
    }
}