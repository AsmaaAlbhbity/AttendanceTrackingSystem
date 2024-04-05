using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NuGet.DependencyResolver;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
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

		public IActionResult ViewAllSchedule(int trackId)
		{
			try
			{
				ViewBag.SelectedTrackId = trackId;
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
			return View();
		}

	}
}
