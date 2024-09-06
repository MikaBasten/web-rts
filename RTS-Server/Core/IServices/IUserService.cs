using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        bool Register(string username, string password);
        string Login(string username, string password);
        bool ValidateUser(string username, string password);
        string HashPassword(string password);

    }
}
