using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
	[Authorize]
	[Authorize(Roles = "Security")]
	[Authorize(Roles = "StudentAffairs")]
	public class EmployeeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
