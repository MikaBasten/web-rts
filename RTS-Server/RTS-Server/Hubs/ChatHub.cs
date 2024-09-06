using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RTS_Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToLobby(int lobbyId, string user, string message)
        {
            // Sends the message to all clients in the specified lobby
            await Clients.Group($"Lobby-{lobbyId}").SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinLobby(int lobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Lobby-{lobbyId}");
        }

        public async Task LeaveLobby(int lobbyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Lobby-{lobbyId}");
        }
    }
}
