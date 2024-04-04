﻿using AttendanceTrackingSystem.IRepository;
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
                    Claim claim5 = new Claim(ClaimTypes.Uri, user.ImgUrl?.ToString() ?? "");
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
					identity.AddClaim(claim1);
					identity.AddClaim(claim2);
					identity.AddClaim(claim3);
					identity.AddClaim(claim4);
					identity.AddClaim(claim5);
					ClaimsPrincipal principal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync(principal);
					TempData["role"] = claim2.Value;
					//ViewBag.ImgUrl = user.ImgUrl ?? "~images/user.png";
					TempData["img"] = user.ImgUrl ?? "/images/user.png";
					EditProfileViewModel model1 = new EditProfileViewModel
					{
						Name = user.Name,
						Email = user.Email,
						Phone = user.Phone,
						ImgUrl = user.ImgUrl ?? "/images/user.png",
						OldPassword = user.Password
					};

					if (user.UserType == "Employee")
						ViewBag.role = repoAccount.GetEmployeeType(user.UserId).ToString();
					else
						ViewBag.role = user.UserType;

                    switch (user.UserType)
                    {
                        case "Student":
                            return RedirectToAction("Home", "Student");
                        case "Instructor":
                            return RedirectToAction("Home", "Home");
                        case "Employee":
                            switch (repoAccount.GetEmployeeType(user.UserId))
                            {
                                case "Admin":
                                    return RedirectToAction("Home", "Admin");
                                case "Security":
                                case "StudentAffairs":

                                    return RedirectToAction("Home", "Home");
                      
             
                                default:
                                    return RedirectToAction("Index", "Home"); // Default fallback
                            }
                        // Add more cases for other user types as needed...
                        default:
                            return RedirectToAction("Index", "Home");
                    }
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
			EditProfileViewModel model1 = new EditProfileViewModel
			{
				Name = model.Name,
				Email = model.Email,
				Phone = model.Phone,
				ImgUrl = model.ImgUrl ?? "/images/user.png",
				OldPassword = model.Password
			};
			ViewBag.img = model1.ImgUrl;
			return View(model1);
		}
		[HttpPost]
		public IActionResult updateImage(IFormFile ImgUrl)
		{
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			string imgPath = string.Empty;
			if (ImgUrl != null)
			{
				imgPath = $"{userId}.{ImgUrl.FileName.Split(".").Last()}";
				using (var fs = new FileStream(("wwwroot/images/" + imgPath), FileMode.Create))
				{
					ImgUrl.CopyTo(fs);
				}
			}
			if (imgPath != string.Empty)
			{
				repoAccount.UpdateImage("/images/" + imgPath, userId);
				TempData.Peek("img");
				TempData["img"] = "/images/" + imgPath;
			}
			//var cacheBuster = DateTime.UtcNow.Ticks;
			//Response.Headers["Cache-Control"] = "no-cache, max-age=0";
			//TempData["CacheBuster"] = cacheBuster;
			return RedirectToAction("Profile");
		}
		[HttpPost]
		public IActionResult Profile(EditProfileViewModel model)
		{
			int c = 0;
			foreach (var key in ModelState.Keys)
			{
				foreach (var error in ModelState[key].Errors)
				{
					Console.WriteLine($"{key}: {error.ErrorMessage}");
					c++;
				}
			}
			if (c <= 1)
			{
				repoAccount.SaveEdit(model);
			}
			return RedirectToAction("Profile");
			//return RedirectToAction("Error", "Home");
		}
		[HttpGet]
		public IActionResult CheckPassword(string OldPassword)
		{
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			User user = repoAccount.GetUserByid(userId);
			return Json(user.Password == OldPassword);

		}
	}
}