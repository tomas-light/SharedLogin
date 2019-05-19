namespace Core.Services.Histories
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Identity;
    using System.Linq;

	using Infrastructure.Repositories;
	using Domain = Core.Models;
	using Data = Infrastructure.Models;
    using AutoMapper;

    public class HistoryService<TUserPrimaryKey> : IHistoryService<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		private readonly IMapper mapper;
		private readonly IHistoryRepository<TUserPrimaryKey> repository;

		private Func<Data.History<TUserPrimaryKey>, Domain.History> mapDataToDomain;

		public HistoryService(
			IMapper mapper,
			IHistoryRepository<TUserPrimaryKey> historyRepository)
		{
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			this.repository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));

			this.mapDataToDomain = this.mapper.Map<Data.History<TUserPrimaryKey>, Domain.History>;
		}

		public async Task<Domain.History> GetByIdAcync(int id)
		{
			var dataHistory = await this.repository.FindByIdAcync(id);
			return mapDataToDomain(dataHistory);
		}

		public async Task<List<Domain.History>> GetByAccountIdAsync(int accountId)
		{
			var histories = await this.repository.FindByAccountIdAsync(accountId);
			return histories.Select(mapDataToDomain).ToList();
		}

		public async Task UpdateLastLogoutTimeAsync(
			Domain.Account<TUserPrimaryKey> account,
			IdentityUser<TUserPrimaryKey> owner,
			IdentityUser<TUserPrimaryKey> accessibleUser)
		{
			var dateTime = DateTime.UtcNow;
			await this.UpdateLastLogoutTimeForAccountAsync(account.Id, dateTime);

			var newAccessHistory = new Data.History<TUserPrimaryKey>
			{
				AccountId = account.Id,
				LoginDateTime = dateTime,
				OwnerName = owner.UserName,
				AccessibleAccountName = accessibleUser.UserName,
			};
			await this.repository.AddAsync(newAccessHistory);
		}

		private async Task UpdateLastLogoutTimeForAccountAsync(int accountId, DateTime dateTime)
		{
			var lastAccessHistory = await this.GetLastHistoryByAccountIdAsync(accountId);
			if (lastAccessHistory != null)
			{
				lastAccessHistory.LogoutDateTime = dateTime;
				await this.repository.UpdateAsync(lastAccessHistory);
			}
		}

		private async Task<Data.History<TUserPrimaryKey>> GetLastHistoryByAccountIdAsync(int accountId)
		{
			var histories = await this.repository.FindByAccountIdAsync(accountId);
			if (!histories.Any())
			{
				return null;
			}

			var lastHistory = histories.OrderByDescending(history => history.LoginDateTime).First();
			return lastHistory;
		}
	}
}
