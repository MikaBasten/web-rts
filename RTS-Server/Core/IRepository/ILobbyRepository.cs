using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ILobbyRepository
    {
        Task<Lobby> CreateLobbyAsync(Lobby lobby);
        Task<Lobby> GetLobbyByIdAsync(int lobbyId);
        Task<IEnumerable<Lobby>> GetAllLobbiesAsync();
        Task<Lobby> UpdateLobbyAsync(Lobby lobby);
        Task<bool> DeleteLobbyAsync(int lobbyId);
        Task<bool> AddPlayerToLobbyAsync(int lobbyId, Player player);
        Task<bool> RemovePlayerFromLobbyAsync(int lobbyId, string userName);
        Task<bool> ToggleReadyStatusAsync(int lobbyId, string username);
    }

}
