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
        Lobby CreateLobbyAsync(string hostUserId, string lobbyName, int playerLimit);
        bool JoinLobby(string lobbyId, string userId, string username);
        bool LeaveLobby(string lobbyId, string userId);
        List<Lobby> GetAllLobbies();
        Lobby GetLobbyById(string lobbyId);
        bool StartGame(string lobbyId);
    }
}
