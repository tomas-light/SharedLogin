using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLogin.Infrastructure.Repositories;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly ISharedAccountsRepository sharedAccountsRepository;

		public HomeController(ISharedAccountsRepository sharedAccountsRepository, ApplicationDbContext applicationDbContext)
		{
			this.sharedAccountsRepository = sharedAccountsRepository;
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<IActionResult> Index()
		{
			var result1 = applicationDbContext.Users.ToList();
			var result = await this.sharedAccountsRepository.FindAllAsync();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
