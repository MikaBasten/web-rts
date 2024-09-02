using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RTS_Server.Hubs
{
    public class GameHub : Hub
    {
        // Method called when a player joins the game
        public async Task JoinGame(string playerName)
        {
            await Clients.All.SendAsync("PlayerJoined", playerName);
        }

        // Method called when a player takes an action (e.g., moving a unit)
        public async Task PlayerAction(string playerId, string action)
        {
            // Handle the player's action (update game state, etc.)
            await Clients.All.SendAsync("ReceivePlayerAction", playerId, action);
        }
    }

}
