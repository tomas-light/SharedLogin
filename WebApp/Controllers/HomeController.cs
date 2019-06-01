namespace WebApp.Controllers
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using Core.Services;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using WebApp.Data;
	using WebApp.Models;

	public class HomeController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IAccountService accountService;

		public HomeController(
			UserManager<IdentityUser> userManager,
			ApplicationDbContext applicationDbContext,
			IAccountService accountService)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
			this.accountService = accountService;
		}

		public IActionResult Index()
		{
			return View("~/Views/Home/App.cshtml");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
