namespace Infrastructure.Repositories
{
    using Infrastructure.Models;
    using System;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHistoryRepository<TUserPrimaryKey> where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		Task<History<TUserPrimaryKey>> FindByIdAcync(int id);

		Task<List<History<TUserPrimaryKey>>> FindByAccountIdAsync(int accountId);

		Task<History<TUserPrimaryKey>> AddAsync(History<TUserPrimaryKey> history);

		Task<History<TUserPrimaryKey>> UpdateAsync(History<TUserPrimaryKey> history);
	}
}
