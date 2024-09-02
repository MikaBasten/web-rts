using Core.IRepository;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher; // Add PasswordHasher

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher; // Inject PasswordHasher
        }

        public bool Register(string username, string password)
        {
            if (_userRepository.GetUserByUsername(username) != null)
                return false;

            User user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password) // Use HashPassword method to hash the password
            };

            _userRepository.AddUser(user);
            return true;
        }

        public bool ValidateUser(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
                return false;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }

        public string HashPassword(string password)
        {
            // Hash the password using ASP.NET Core Identity
            return _passwordHasher.HashPassword(null, password);
        }
    }
}
