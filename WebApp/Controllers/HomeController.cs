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
		private readonly IAccountService accountService;

		public HomeController(
			UserManager<IdentityUser> userManager,
			ApplicationDbContext applicationDbContext,
			IAccountService accountService)
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

			var account = await this.accountService.AddAsync(user.Id);
			var accessibleAccounts = await this.accountService.GetAccessibleAccountsAsync();
			await this.accountService.ActivateAccountByIdAsync(user.Id);
			var activatedUserId = await this.accountService.GetActivatedAccountIdAsync();
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}


	public class IdentityUser<TKey> where TKey : IEquatable<TKey>
	{
		public virtual TKey Id { get; set; }

		public virtual string UserName { get; set; }

		public virtual string NormalizedUserName { get; set; }

		public virtual string Email { get; set; }

		public virtual string NormalizedEmail { get; set; }

		public virtual bool EmailConfirmed { get; set; }

		public virtual string PasswordHash { get; set; }

		public virtual string SecurityStamp { get; set; }

		public virtual string ConcurrencyStamp { get; set; }

		public virtual string PhoneNumber { get; set; }

		public virtual bool PhoneNumberConfirmed { get; set; }

		public virtual bool TwoFactorEnabled { get; set; }

		public virtual DateTimeOffset? LockoutEnd { get; set; }

		public virtual bool LockoutEnabled { get; set; }

		public virtual int AccessFailedCount { get; set; }
	}
}
