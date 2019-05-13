namespace WebApp.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using SharedLogin.Infrastructure.Repositories;
	using WebApp.Models;

	public class HomeController : Controller
	{
		private readonly ISharedAccountsRepository sharedAccountsRepository;

		public HomeController(ISharedAccountsRepository sharedAccountsRepository)
		{
			this.sharedAccountsRepository = sharedAccountsRepository;
		}

		public async Task<IActionResult> Index()
		{
			var res = await this.sharedAccountsRepository.FindAllAsync();
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
