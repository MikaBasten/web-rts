using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Player
    {
        public string UserId { get; set; } // User ID of the player
        public string Username { get; set; } // Username of the player
        public bool IsReady { get; set; } = false; // Ready status of the player
    }
}
