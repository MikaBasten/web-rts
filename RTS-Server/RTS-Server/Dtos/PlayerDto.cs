using Core.Models;

namespace RTS_Server.Dtos
{
    public class PlayerDto
    {
        public string Username { get; set; } // The username of the player
        public bool IsReady { get; set; } // Ready status of the player
        public Faction SelectedFaction { get; set; } // The faction the player selected
    }
}
