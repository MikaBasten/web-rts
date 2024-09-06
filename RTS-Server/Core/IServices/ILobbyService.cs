using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface ILobbyService
    {
        Task<Lobby> CreateLobbyAsync(string hostUserName, string lobbyName, int playerLimit);
        Task<bool> JoinLobbyAsync(int lobbyId, string userName);
        Task<bool> LeaveLobbyAsync(int lobbyId, int userId);
        Task<IEnumerable<Lobby>> GetAllLobbiesAsync();
        Task<Lobby> GetLobbyByIdAsync(int lobbyId);
        Task<bool> StartGameAsync(int lobbyId);
    }
}
