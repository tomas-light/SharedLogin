namespace Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
	using System.Collections.Generic;
	using System.Text;

	abstract class DependencyConfiguration<TUserPrimaryKey>
		 where TUserPrimaryKey : IEquatable<TUserPrimaryKey>
	{
		public static void Configure(IServiceCollection services)
		{
		}
	}
}
