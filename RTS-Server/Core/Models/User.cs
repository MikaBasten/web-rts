using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // Store hashed passwords for security
        public int Faction { get; set; }
        public bool IsReady { get; set; } = false;

    }
}
