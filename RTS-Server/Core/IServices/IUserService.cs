using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IUserService
    {
        bool Register(string username, string password);
        bool ValidateUser(string username, string password);
        string HashPassword(string password);
    }
}
