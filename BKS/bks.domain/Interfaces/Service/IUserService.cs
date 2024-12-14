using BKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces.Service
{
	public interface IUserService
	{
		Task<User> AuthenticateUserAsync(string email, string password);
		Task RegisterUserAsync(User user);
		Task<User?> GetProfileAsync(Guid userId);
		Task<User?> GetUserByIdAsync(Guid userId);
		Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
		Task<bool> ResetPasswordAsync(string email);
	}
}
