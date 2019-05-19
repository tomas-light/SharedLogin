namespace Core.Services
{
	using Core.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHistoryService<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		Task<History> GetByIdAcync(int id);

		Task<List<History>> GetByAccountIdAsync(int accountId);

		Task UpdateLastLogoutTimeAsync(
			Account<TUserPrimaryKey> account,
			IdentityUser<TUserPrimaryKey> owner,
			IdentityUser<TUserPrimaryKey> accessibleUser);
	}
}
