using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Models
{
    public class Lobby
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Unique identifier for the lobby
        public string Name { get; set; } // Name of the lobby
        public string HostUserId { get; set; } // User ID of the player who created the lobby
        public List<Player> Players { get; set; } = new List<Player>(); // List of players in the lobby
        public int PlayerLimit { get; set; } // Maximum number of players allowed in the lobby
        public bool IsGameStarted { get; set; } = false; // Status to check if the game has started
    }
}
