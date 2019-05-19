namespace Infrastructure.Repositories
{
    using Infrastructure.Entities;
    using System;
	using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHistoryRepository
	{
		Task<History> FindByIdAcync(int id);

		Task<List<History>> FindByAccountIdAsync(int accountId);

		Task<History> AddAsync(History history);

		Task<History> UpdateAsync(History history);
	}
}
