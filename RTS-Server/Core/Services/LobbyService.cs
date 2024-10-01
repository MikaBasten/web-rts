using Core.IRepository;
using Core.IServices;
using Core.Models;
using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;

        public LobbyService(ILobbyRepository lobbyRepository, IUserRepository userRepository)
        {
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
        }

        public async Task<Lobby> CreateLobbyAsync(string hostUsername, string lobbyName, int playerLimit)
        {
            User hostUser = _userRepository.GetUserByUsername(hostUsername);
            if (hostUser == null)
                return null; // User not found

            var lobby = new Lobby
            {
                Name = lobbyName,
                HostUserId = hostUser.Id,
                PlayerLimit = playerLimit,
                Players = new List<Player>
                {
                    new Player { User = hostUser, IsReady = false, SelectedFaction = Faction.None }
                }
            };

            return await _lobbyRepository.CreateLobbyAsync(lobby);
        }

        public async Task<bool> JoinLobbyAsync(int lobbyId, string userName)
        {
            var user = _userRepository.GetUserByUsername(userName);
            if (user == null)
                return false; // User not found

            return await _lobbyRepository.AddPlayerToLobbyAsync(lobbyId, new Player { User = user, IsReady = false, SelectedFaction = Faction.None });
        }

        public async Task<bool> LeaveLobbyAsync(int lobbyId, string username)
        {
            return await _lobbyRepository.RemovePlayerFromLobbyAsync(lobbyId, username);
        }

        public async Task<IEnumerable<Lobby>> GetAllLobbiesAsync()
        {
            return await _lobbyRepository.GetAllLobbiesAsync();
        }

        public async Task<Lobby> GetLobbyByIdAsync(int lobbyId)
        {
            return await _lobbyRepository.GetLobbyByIdAsync(lobbyId);
        }
        public async Task<bool> ToggleReadyStatusAsync(int lobbyId, string username)
        {
            return await _lobbyRepository.ToggleReadyStatusAsync(lobbyId, username);
        }
        public async Task<bool> StartGameAsync(int lobbyId)
        {
            var lobby = await _lobbyRepository.GetLobbyByIdAsync(lobbyId);
            if (lobby == null || lobby.Players.Count < 2)
                return false; // Game can't start without at least two players

            lobby.IsGameStarted = true;
            await _lobbyRepository.UpdateLobbyAsync(lobby);
            return true;
        }
    }
}
