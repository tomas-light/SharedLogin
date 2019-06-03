namespace WebApp.Data
{
    using Microsoft.AspNetCore.Identity;
	using System.Threading.Tasks;

	public class DbContextInitializer
	{
		private readonly ApplicationDbContext dbContext;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;

		public DbContextInitializer(
			ApplicationDbContext dbContext,
			UserManager<User> userManager,
			RoleManager<Role> roleManager)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public async Task Initialize()
		{
			this.dbContext.Database.EnsureCreated();

			await this.CreateManagers();
			await this.CreateProfessors();
			await this.CreateStudents();
		}

		private async Task CreateManagers()
		{
			var itManagerRole = await this.CreateRole("IT Department manager");
			var ufcManagerRole = await this.CreateRole("UFC Department manager");

			await this.CreateUser(
				"management@study.com",
				"Richard Brown",
				"/img/richard.png",
				"Qwe!23",
				itManagerRole);

			await this.CreateUser(
				"ufc-manager@study.com",
				"Brian Murphy",
				"/img/brian.png",
				"Qwe!23",
				ufcManagerRole);
		}

		private async Task CreateProfessors()
		{
			var professorManagerRole = await this.CreateRole("Professor");

			await this.CreateUser(
				"it-professor@study.com",
				"David Parker",
				"/img/david.png",
				"Qwe!23",
				professorManagerRole);

			await this.CreateUser(
				"ufc-professor@study.com",
				"Rachel Cole",
				"/img/rachel.png",
				"Qwe!23",
				professorManagerRole);
		}

		private async Task CreateStudents()
		{
			var studentManagerRole = await this.CreateRole("Student");

			await this.CreateUser(
				"student-1@study.com",
				"Alice Davis",
				"/img/alice.png",
				"Qwe!23",
				studentManagerRole);

			await this.CreateUser(
				"student-2@study.com",
				"Archie Coleman",
				"/img/archie.png",
				"Qwe!23",
				studentManagerRole);

			await this.CreateUser(
				"student-3@study.com",
				"Callum Adams",
				"/img/callum.png",
				"Qwe!23",
				studentManagerRole);

			await this.CreateUser(
				"student-4@study.com",
				"Nancy Walker",
				"/img/nancy.png",
				"Qwe!23",
				studentManagerRole);
		}


		private async Task<Role> CreateRole(string name)
		{
			var role = new Role(name);
			await this.roleManager.CreateAsync(role);
			return role;
		}

		private async Task CreateUser(
			string email, 
			string name, 
			string avatar, 
			string password, 
			Role role)
		{
			var user = new User
			{
				NormalizedUserName = email.ToUpper(),
				UserName = email,
				Email = email,
				NormalizedEmail = email.ToUpper(),
				Name = name,
				Avatar = avatar,
				EmailConfirmed = true,
			};

			await this.userManager.CreateAsync(user);
			await userManager.AddToRoleAsync(user, role.Name);

			var resetToken = await this.userManager.GeneratePasswordResetTokenAsync(user);
			await this.userManager.ResetPasswordAsync(user, resetToken, password);

			await this.dbContext.SaveChangesAsync();
		}
	}
}
