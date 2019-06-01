namespace WebApp.Api
{
	using System.Linq;
	using System.Threading.Tasks;
    using AutoMapper;
    using Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Data;
    using WebApp.Models.Account.Response;

    [Authorize]
	[Route("api/[controller]/[action]")]
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IAccountService accountService;
		private readonly IMapper mapper;

		public AccountController(
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ApplicationDbContext applicationDbContext,
			IAccountService accountService,
			IMapper mapper)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
			this.accountService = accountService;
			this.mapper = mapper;
		}

		[Route("auth")]
		[HttpGet]
		public async Task<IActionResult> GetAuthenticatedAccount()
		{
			var authenticatedAccountId = await this.accountService.GetAuthenticatedAccountIdAsync();
			if (string.IsNullOrEmpty(authenticatedAccountId))
			{
				return BadRequest();
			}

			var authenticatedAccount = await this.userManager.FindByIdAsync(authenticatedAccountId);
			if (authenticatedAccount == null)
			{
				return BadRequest();
			}

			var roleName = await this.accountService.GetAuthenticatedAccountRoleNameAsync();
			if (roleName == null)
			{
				return BadRequest();
			}

			var activeRole = await this.roleManager.FindByNameAsync(roleName);
			if (activeRole == null)
			{
				return BadRequest();
			}

			var authenticatedAccountDTO = this.mapper.Map<IdentityUser, AccountDTO>(authenticatedAccount);
			this.mapper.Map(activeRole, authenticatedAccountDTO);

			return Ok(authenticatedAccountDTO);
		}

		[Route("active")]
		[HttpGet]
		public async Task<IActionResult> GetActiveAccount()
		{
			var accountId = await this.accountService.GetActivatedAccountIdAsync();
			if (string.IsNullOrEmpty(accountId))
			{
				return BadRequest();
			}

			var activeAccount = await this.userManager.FindByIdAsync(accountId);
			if (activeAccount == null)
			{
				return BadRequest();
			}

			var roleId = await this.accountService.GetActivatedAccountRoleIdAsync();
			if (roleId == null)
			{
				return BadRequest();
			}

			var activeRole = await this.roleManager.FindByIdAsync(roleId);
			if (activeRole == null)
			{
				return BadRequest();
			}

			var activeAccountDTO = this.mapper.Map<IdentityUser, AccountDTO>(activeAccount);
			this.mapper.Map(activeRole, activeAccountDTO);

			return Ok(activeAccountDTO);
		}

		public async Task<IActionResult> Index()
		{
			// var result1 = applicationDbContext.Users.ToList();
			// var result = await this.accountService.GetActivatedAccountIdAsync();

			var currentUser = await this.userManager.GetUserAsync(HttpContext.User);
			var user = applicationDbContext.Users.FirstOrDefault(u => u.Id != currentUser.Id);
			if (user == null)
			{
				return BadRequest();
			}

			var account = await this.accountService.AddAsync(user.Id);
			var accessibleAccounts = await this.accountService.GetAccessibleAccountsAsync();
			await this.accountService.ActivateAccountByIdAsync(user.Id);
			var activatedUserId = await this.accountService.GetActivatedAccountIdAsync();

			return Ok();
		}
    }
}