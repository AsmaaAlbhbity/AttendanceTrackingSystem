using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Repository;
using AttendanceTrackingSystem.ViewModel;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
	public class AccountController : Controller
	{
		IRepoAccount repoAccount;
		public AccountController(IRepoAccount _repoAccount)
		{
			repoAccount = _repoAccount;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = repoAccount.GetUser(model.Email, model.Password);
				if (user != null)
				{
					Claim claim1 = new Claim(ClaimTypes.Name, user.Name);
					Claim claim2;
					if (user.UserType != "Employee")
					{
						claim2 = new Claim(ClaimTypes.Role, user.UserType);
					}
					else
					{
						claim2 = new Claim(ClaimTypes.Role, repoAccount.GetEmployeeType(user.UserId));
					}
					Claim claim3 = new Claim(ClaimTypes.Email, user.Email);
					Claim claim4 = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());
					ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
					identity.AddClaim(claim1);
					identity.AddClaim(claim2);
					identity.AddClaim(claim3);
					identity.AddClaim(claim4);
					ClaimsPrincipal principal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync(principal);
					TempData["role"] = claim2.Value;
					ViewBag.img = user.ImgUrl ?? "~images/user.png";

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid Email Or password");
				}
			}
			return View();
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}
		public IActionResult Profile()
		{
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			User model = repoAccount.GetUserByid(userId);
			
			if (model.ImgUrl == null)
				ViewBag.img = "/images/user.png";
			else
				ViewBag.img = model.ImgUrl;

			if (model.UserType == "Employee")
				ViewBag.role = repoAccount.GetEmployeeType(userId).ToString();
			else
				ViewBag.role = model.UserType;
			
			return View(model);
		}

	}
}
