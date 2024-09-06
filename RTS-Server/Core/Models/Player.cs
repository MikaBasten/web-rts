using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Player
    {
        public int Id { get; set; } // Unique player ID, separate from User ID
        public User User { get; set; } // Reference to the user
        public bool IsReady { get; set; } // Whether the player is ready in the lobby
        public Faction SelectedFaction { get; set; } = Faction.None; // Faction chosen by the player
    }

}
