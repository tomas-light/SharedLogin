namespace WebApp.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
    using AutoMapper;
    using Core.Models;
    using Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Data;
    using WebApp.Models.History.Response;

    [Authorize]
	[Route("api/history")]
	public class HistoryController : Controller
	{
		private readonly ApplicationDbContext dbContext;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;
		private readonly IAccountService<User, Role, string> accountService;
		private readonly IHistoryService historyService;
		private readonly IMapper mapper;

		public HistoryController(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			ApplicationDbContext applicationDbContext,
			IAccountService<User, Role, string> accountService,
			IHistoryService historyService,
			IMapper mapper)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.dbContext = applicationDbContext;
			this.accountService = accountService;
			this.historyService = historyService;
			this.mapper = mapper;
		}

		[Route("access")]
		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var currentUserId = await this.accountService.GetAuthenticatedAccountIdAsync();
			var accountsWithAccess = await this.accountService.GetAccountsThatHaveAccess(currentUserId);
			var accountsId = accountsWithAccess.Select(u => u.Id).ToArray();

			var histories = await this.historyService.GetByAccountIdsAsync(accountsId);

			var accessHistories = from _account in accountsWithAccess
								  join _history in histories on _account.Id equals _history.Id
								  select this.MapToAccessHistory(_account, _history);

			var dto = new AccessHistoriesDTO
			{
				Histories = accessHistories.ToArray()
			};

			return Ok(dto);
		}

		private AccessHistoryDTO MapToAccessHistory(Account account, History history)
		{
			var accessHistory = this.mapper.Map<History, AccessHistoryDTO>(history);
			this.mapper.Map(account, accessHistory);
			return accessHistory;
		}
	}
}