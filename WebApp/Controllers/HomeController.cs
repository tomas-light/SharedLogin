using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLogin.Infrastructure.Repositories;
using SharedLogin.Services;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly ISharedAccountsService sharedAccountsService;

		public HomeController(ISharedAccountsService sharedAccountsService, ApplicationDbContext applicationDbContext)
		{
			this.sharedAccountsService = sharedAccountsService ?? throw new ArgumentNullException(nameof(sharedAccountsService));
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<IActionResult> Index()
		{
			var result1 = applicationDbContext.Users.ToList();
			var result = await this.sharedAccountsService.GetSharedAccountsByCurrentUserAsync();
			return View();
		}

		public async Task<IActionResult> Privacy()
		{
			const string accountId = "98d717d5-c1eb-4349-9b3b-ad0362e0e695";
			var sharedAccount = await this.sharedAccountsService.AddAsync(accountId);
			await this.sharedAccountsService.SetCurrentAccountIdAsync(accountId);
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
