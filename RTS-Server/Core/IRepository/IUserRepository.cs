﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        User GetUserByUsername(string username);
        void AddUser(User user);
    }
}
