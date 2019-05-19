namespace Core.Services
{
	using Core.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHistoryService
	{
		Task<History> GetByIdAcync(int id);

		Task<List<History>> GetByAccountIdAsync(int accountId);

		Task UpdateLastLogoutTimeAsync(
			Account account,
			IdentityUser owner,
			IdentityUser accessibleUser);
	}
}
