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
	using WebApp.Models.Auth;

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

				var dto = new AccountsInformationDTO
				{
					AuthenticatedAccount = authenticatedAccountDTO,
					ActiveAccount = activeAccountDTO,
					AccessibleAccounts = accessibleAccountsDTO,
				};

				return Ok(dto);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
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

			var role = await this.roleManager.FindByIdAsync(roleId);
			if (role == null)
			{
				throw new NullReferenceException(nameof(role));
			}

			var activeAccountDTO = this.mapper.Map<User, AccountDTO>(activeAccount);
			this.mapper.Map(role, activeAccountDTO);

			return activeAccountDTO;
		}

		private async Task<AccountDTO[]> GetAccessibleAccountsInfo()
		{
			var accessibleAccounts = await this.accountService.GetAccessibleAccountsAsync();
			if (!accessibleAccounts.Any())
			{
				throw new Exception("accessible accounts is empty");
			}

			var accountIds = accessibleAccounts.Select(account => account.AccessibleAccountId).ToList();
			var accessibleAccountsDTO = this.GetAccountsDTO()
											.Where(user => accountIds.Contains(user.Id))
											.ToArray();

			return accessibleAccountsDTO;
		}

		private IQueryable<AccountDTO> GetAccountsDTO()
		{
			var data = from userRole in this.dbContext.UserRoles
					   join user in this.dbContext.Users on userRole.UserId equals user.Id
					   join role in this.dbContext.Roles on userRole.RoleId equals role.Id
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
			return data;
		}

		[Route("activate")]
		[HttpPost]
		public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var token = new AuthJwtTokenDTO();
			try
			{
				token.Token = await this.accountService.ActivateAccountByIdAsync(model.AccountId);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok(token);
		}

		[Route("users")]
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var allUsers = this.GetAccountsDTO().ToArray();

			var currentUserId = await this.accountService.GetAuthenticatedAccountIdAsync();
			var usersWithAccess = await this.accountService.GetAccountsThatHaveAccess(currentUserId);
			var usersId = usersWithAccess.Select(u => u.UserId);

			var usersThatHaveAccess = allUsers.Where(user => usersId.Contains(user.Id)).ToArray();

			var dto = new UsersDTO
			{
				Users = allUsers,
				UsersThatHaveAccess = usersThatHaveAccess,
			};

			return Ok(dto);
		}

		[Route("access")]
		[HttpPost]
		public async Task<IActionResult> AddAccess([FromBody] ActivateAccountDTO model)
		{
			try
			{
				await this.accountService.AddAsync(model.AccountId);
				return Ok(model);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("access/{accountId}")]
		[HttpDelete]
		public async Task<IActionResult> RemoveAccess(string accountId)
		{
			try
			{
				await this.accountService.DeleteAsync(accountId);
				return Ok(accountId);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Get a list of accounts that have access to the current account
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		[Route("access")]
		[HttpGet]
		public async Task<IActionResult> GetAccountsThatHaveAccess()
		{
			var currentUserId = await this.accountService.GetAuthenticatedAccountIdAsync();
			var accountsWithAccess = await this.accountService.GetAccountsThatHaveAccess(currentUserId);
			var accessibleAccountsId = accountsWithAccess.Select(u => u.AccessibleAccountId).ToList();

			var allUsers = this.GetAccountsDTO();

			var accounts = allUsers.Where(user => accessibleAccountsId.Contains(user.Id)).ToArray();

			var dto = new AccountsWithAccessDTO
			{
				Accounts = accounts,
			};

			return Ok(dto);
		}
	}
}