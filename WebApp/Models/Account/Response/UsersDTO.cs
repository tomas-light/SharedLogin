﻿namespace WebApp.Models.Account.Response
{
	public class UsersDTO
	{
		public AccountDTO[] Users { get; set; }

		public AccountDTO[] UsersThatHaveAccess { get; set; }
	}
}
