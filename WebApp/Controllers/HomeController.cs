using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IAccountService<string> accountService;

		public HomeController(
			UserManager<IdentityUser> userManager, 
			ApplicationDbContext applicationDbContext,
			IAccountService<string> accountService)
		{
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
			this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
		}

		public async Task<IActionResult> Index()
		{
			var result1 = applicationDbContext.Users.ToList();
			var result = await this.accountService.GetActivatedAccountIdAsync();
			return View();
		}

		public async Task<IActionResult> Privacy()
		{
			var currentUser = await this.userManager.GetUserAsync(HttpContext.User);
			var user = applicationDbContext.Users.FirstOrDefault(u => u.Id != currentUser.Id);
			if (user == null)
			{
				return View();
			}

			var sharedAccount = await this.accountService.AddAsync(user.Id);
			await this.accountService.ActivateAccountIdAsync(user.Id);
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
