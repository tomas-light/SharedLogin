namespace WebApp.Controllers
{
	using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Models.Auth;

    [Route("api/[controller]/[action]")]
	[Authorize]
	public class AuthController : Controller
	{
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AuthController(
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var user = await this.userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return Unauthorized();
			}

			var signInResult = await this.PasswordSignInAsync(model.Email, model.Password);
			if (signInResult.Succeeded)
			{
				return Ok();
			}

			return Unauthorized();
		}

		private Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(string email, string password)
		{
			const bool isPersistent = true;
			const bool lockoutOnFailure = false;

			return signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();
			Response.Cookies.Delete(".AspNetCore.Identity.Application");
			return Ok();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterUserDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var user = await this.userManager.FindByEmailAsync(model.Email);
			if (user != null)
			{
				return Conflict();
			}

			var role = await this.roleManager.FindByIdAsync(model.RoleId);
			if (role == null)
			{
				return BadRequest();
			}

			user = new IdentityUser()
			{
				Email = model.Email,
				UserName = model.Name
			};
			var identityResult = await userManager.CreateAsync(user);
			if (!identityResult.Succeeded)
			{
				return BadRequest(identityResult.Errors);
			}

			identityResult = await userManager.AddToRoleAsync(user, role.Name);
			if (!identityResult.Succeeded)
			{
				return BadRequest(identityResult.Errors);
			}

			var signInResult = await this.PasswordSignInAsync(model.Email, model.Password);
			if (signInResult.Succeeded)
			{
				return Ok();
			}

			return Unauthorized();
		}

		[Route("role")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> CreateRole(RegisterRoleDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var role = await this.roleManager.FindByNameAsync(model.Name);
			if (role != null)
			{
				return Conflict();
			}

			role = new IdentityRole(model.Name);
			var identityResult = await this.roleManager.CreateAsync(role);
			if (identityResult.Succeeded)
			{
				return Ok();
			}

			return BadRequest(identityResult.Errors);
		}
	}
}