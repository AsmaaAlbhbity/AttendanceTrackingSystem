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
		public IActionResult ShowTrackSchedule(int? trackId)
		{
			try
			{
				int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				List<Track> InstTracks = repoInstructor.GetInstructorTracks(userId);
				if (trackId != null)
				{
					ViewBag.InstructorTracks = InstTracks;
					ViewBag.SelectedTrackId = trackId;
					List<Schedule> WeekSchedule = repoSchedule.GetWeeklyScheduleForTrack(trackId.Value);
					return View(WeekSchedule);
				}
				else // First Time opening the page
				{
					trackId = InstTracks.FirstOrDefault().TrackId;
				}

				ViewBag.InstructorTracks = InstTracks.ToList();
				ViewBag.IsSupervisor = repoInstructor.IsSuperisor(userId);
				if (InstTracks.Any(t => t.TrackId == trackId))
				{
					List<Schedule> WeekSchedule = repoSchedule.GetWeeklyScheduleForTrack(trackId.Value);
					ViewBag.InstructorTracks = InstTracks;
					ViewBag.SelectedTrackName = InstTracks.Where(a => a.TrackId == trackId).Select(a => a.Name);
					return View(WeekSchedule);
				}
				else
				{
					return View("Error", "Home");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred in Home action");
				return View("Error");
			}
		}
		public IActionResult AddSchedule()
		{
			return View();
		}

	}
}
