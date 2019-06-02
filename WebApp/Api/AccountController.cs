namespace WebApp.Api
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
    using AutoMapper;
    using Core.Services;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using WebApp.Data;
    using WebApp.Models.Account.Request;
	using WebApp.Models.Account.Response;

	[Authorize]
	[Route("api/account")]
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext dbContext;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;
		private readonly IAccountService<User, Role, string> accountService;
		private readonly IMapper mapper;

		public AccountController(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			ApplicationDbContext applicationDbContext,
			IAccountService<User, Role, string> accountService,
			IMapper mapper)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.dbContext = applicationDbContext;
			this.accountService = accountService;
			this.mapper = mapper;
		}

		[Route("get-info")]
		[HttpPost]
		public async Task<IActionResult> GetCurrentInformation()
		{
			try
			{
				var authenticatedAccountDTO = await this.GetAuthenticatedAccountInfo();
				var activeAccountDTO = await this.GetActiveAccountInfo();
				var accessibleAccountsDTO = await this.GetAccessibleAccountsInfo();

				return Ok(new
				{
					authenticatedAccount = authenticatedAccountDTO,
					activeAccount = activeAccountDTO,
					accessibleAccounts = accessibleAccountsDTO,
				}
				);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[Route("auth")]
		[HttpGet]
		public async Task<IActionResult> GetAuthenticatedAccount()
		{
			try
			{
				var authenticatedAccountDTO = await this.GetAuthenticatedAccountInfo();
				return Ok(authenticatedAccountDTO);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private async Task<AccountDTO> GetAuthenticatedAccountInfo()
		{
			var authenticatedAccountId = await this.accountService.GetAuthenticatedAccountIdAsync();
			if (string.IsNullOrEmpty(authenticatedAccountId))
			{
				throw new Exception("authenticated account id in claims not found");
			}

			var authenticatedAccount = await this.userManager.FindByIdAsync(authenticatedAccountId);
			if (authenticatedAccount == null)
			{
				throw new NullReferenceException(nameof(authenticatedAccount));
			}

			var roleName = await this.accountService.GetAuthenticatedAccountRoleNameAsync();
			if (roleName == null)
			{
				throw new NullReferenceException(nameof(roleName));
			}

			var activeRole = await this.roleManager.FindByNameAsync(roleName);
			if (activeRole == null)
			{
				throw new NullReferenceException(nameof(activeRole));
			}

			var authenticatedAccountDTO = this.mapper.Map<User, AccountDTO>(authenticatedAccount);
			this.mapper.Map(activeRole, authenticatedAccountDTO);

			return authenticatedAccountDTO;
		}

		[Route("active")]
		[HttpGet]
		public async Task<IActionResult> GetActiveAccount()
		{
			try
			{
				var activeAccountDTO = await this.GetActiveAccountInfo();
				return Ok(activeAccountDTO);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private async Task<AccountDTO> GetActiveAccountInfo()
		{
			var accountId = await this.accountService.GetActivatedAccountIdAsync();
			if (string.IsNullOrEmpty(accountId))
			{
				throw new Exception("active account id in claims not found");
			}

			var activeAccount = await this.userManager.FindByIdAsync(accountId);
			if (activeAccount == null)
			{
				throw new NullReferenceException(nameof(activeAccount));
			}

			var roleId = await this.accountService.GetActivatedAccountRoleIdAsync();
			if (roleId == null)
			{
				throw new NullReferenceException();
			}

			var activeRole = await this.roleManager.FindByIdAsync(roleId);
			if (activeRole == null)
			{
				throw new NullReferenceException(nameof(activeRole));
			}

			var activeAccountDTO = this.mapper.Map<User, AccountDTO>(activeAccount);
			this.mapper.Map(activeRole, activeAccountDTO);

			return activeAccountDTO;
		}

		[Route("active")]
		[HttpGet]
		public async Task<IActionResult> GetAccessibleAccounts()
		{
			try
			{
				var accessibleAccountsDTO = await this.GetAccessibleAccountsInfo();
				return Ok(accessibleAccountsDTO);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private async Task<AccountDTO[]> GetAccessibleAccountsInfo()
		{
			var accessibleAccounts = await this.accountService.GetAccessibleAccountsAsync();
			if (!accessibleAccounts.Any())
			{
				throw new Exception("accessible accounts is empty");
			}

			var accountIds = accessibleAccounts.Select(account => account.AccessibleAccountId).ToList();

			var data = from userRole in this.dbContext.UserRoles
					   join user in this.dbContext.Users on userRole.UserId equals user.Id
					   join role in this.dbContext.Roles on userRole.RoleId equals role.Id
					   where accountIds.Contains(user.Id)
					   select
						   new AccountDTO
						   {
							   Id = user.Id,
							   Name = user.Name,
							   Email = user.Email,
							   Avatar = user.Avatar,
							   RoleId = role.Id,
							   RoleName = role.Name,
						   };

			var accessibleAccountsDTO = data.ToArray();

			return accessibleAccountsDTO;
		}

		[Route("activate")]
		[HttpPost]
		public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				await this.accountService.ActivateAccountByIdAsync(model.AccountId);
			}
			catch (NullReferenceException ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}
	}
}