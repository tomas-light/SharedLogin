﻿namespace WebApp.Controllers
{
	using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Data;
    using WebApp.Models.Auth;

    [Route("api/auth")]
	[Authorize]
	public class AuthController : Controller
	{
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;

		public AuthController(
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			RoleManager<Role> roleManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		[Route("login")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var user = await this.userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return NotFound("User not found");
			}

			var signInResult = await this.PasswordSignInAsync(model.Email, model.Password);
			if (signInResult.Succeeded)
			{
				return Ok();
			}

			return BadRequest(identityResult.Errors);
		}

		private Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(string email, string password)
		{
			const bool isPersistent = true;
			const bool lockoutOnFailure = false;

			return signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
		}

		[Route("logout")]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();
			Response.Cookies.Delete(".AspNetCore.Identity.Application");
			return Ok();
		}

		[Route("register")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
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
				return NotFound("User not found");
			}

			user = new User()
			{
				Email = model.Email,
				UserName = model.Email,
				EmailConfirmed = true,
				Name = model.Name
			};
			var identityResult = await userManager.CreateAsync(user, model.Password);
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

			return BadRequest(identityResult.Errors);
		}

		[Route("role")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> CreateRole([FromBody] RegisterRoleDTO model)
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

			role = new Role(model.Name);
			var identityResult = await this.roleManager.CreateAsync(role);
			if (identityResult.Succeeded)
			{
				return Ok();
			}

			return BadRequest(identityResult.Errors);
		}
	}
}