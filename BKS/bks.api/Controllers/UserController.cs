using bks.api.Utilities;
using bks.domain.DTOs.User;
using bks.domain.Interfaces.Service;
using bks.inftasturcture.Service;
using BKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bks.api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly JwtTokenHelper _jwtTokenHelper;
		public UserController(IUserService userService, JwtTokenHelper jwtTokenHelper)
		{
			_userService = userService;
			_jwtTokenHelper = jwtTokenHelper;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] User user)
		{
			var existingUser = await _userService.AuthenticateUserAsync(user.Email, user.PasswordHash);
			if (existingUser != null)
				return BadRequest("Email is already registered.");

			await _userService.RegisterUserAsync(user);
			return Ok("User registered successfully.");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto login)
		{
			var user = await _userService.AuthenticateUserAsync(login.Email, login.PasswordHash);
			if (user is null)
				return Unauthorized("Invalid credentials.");

			var token = _jwtTokenHelper.GenerateToken(user.UserId, user.Email);
			return Ok(new { Token = token });
		}

		[Authorize]
		[HttpGet("profile")]
		public async Task<IActionResult> GetProfile()
		{
			var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
			var user = await _userService.GetProfileAsync(userId);
			if (user == null)
				return NotFound("User not found.");

			return Ok(user);
		}

		[Authorize]
		[HttpPost("password/change")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
		{
			var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
			var result = await _userService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);

			if (!result)
				return BadRequest("Old password is incorrect.");

			return Ok("Password changed successfully.");
		}

		//(Mock Implementation)
		[HttpPost("password/reset")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
		{
			var success = await _userService.ResetPasswordAsync(request.Email);

			if (!success)
				return BadRequest("Email not found or verification failed.");

			return Ok("Password reset link sent to your email.");
		}
	}
}

