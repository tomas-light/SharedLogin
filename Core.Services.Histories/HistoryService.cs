namespace Core.Services.Histories
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Linq;

	using Infrastructure.Repositories;
	using Domain = Core.Models;
	using Data = Infrastructure.Entities;
	using AutoMapper;

	public class HistoryService : IHistoryService
	{
		private readonly IMapper mapper;
		private readonly IHistoryRepository repository;

		private Func<Data.History, Domain.History> mapDataToDomain;

		public HistoryService(
			IMapper mapper,
			IHistoryRepository historyRepository)
		{
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			this.repository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));

			this.mapDataToDomain = this.mapper.Map<Data.History, Domain.History>;
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

		public async Task<List<Domain.History>> GetByAccountIdsAsync(int[] accountIds)
		{
			var histories = await this.repository.FindByAccountIdsAsync(accountIds);
			return histories.Select(mapDataToDomain).ToList();
		}

		public async Task<Domain.History> AddAsync(
			Domain.Account account,
			string userName,
			string accessibleAccountNamem,
			DateTime loginDateTime)
		{
			var newHistory = new Data.History
			{
				AccountId = account.Id,
				LoginDateTime = loginDateTime,
				UserName = userName,
				AccessibleAccountName = accessibleAccountNamem,
			};
			await this.repository.AddAsync(newHistory);

			return mapDataToDomain(newHistory);
		}

		public async Task UpdateLastLogoutTimeAsync(Domain.Account account, DateTime logoutDateTime)
		{
			await this.UpdateLastLogoutTimeForAccountAsync(account.Id, logoutDateTime);
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

		private async Task<Data.History> GetLastHistoryByAccountIdAsync(int accountId)
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
