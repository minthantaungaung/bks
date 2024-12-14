using bks.domain.Interfaces.Repository;
using bks.domain.Interfaces.Service;
using BKS.Models;
using System.Security.Cryptography;
using System.Text;

namespace bks.inftasturcture.Service
{
    public class UserService : IUserService
	{
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;
            return user;
        }

        public async Task RegisterUserAsync(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<User?> GetProfileAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !VerifyPassword(oldPassword, user.PasswordHash))
                return false;

            user.PasswordHash = HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }

        // Reset password (Mock logic)
        public async Task<bool> ResetPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return false;

            var emailSent = SendVerifyEmail(user.Email);
            if (!emailSent)
                return false;

            return true;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private static bool VerifyPassword(string inputPassword, string storedPassword)
        {
            var pwd = HashPassword(inputPassword);
            return HashPassword(inputPassword) == storedPassword;
        }
        private static bool SendVerifyEmail(string email)
        {
            return true;
        }
    }
}
