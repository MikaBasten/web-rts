using Core.IRepository;
using Core.IServices;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;

        public LobbyService(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Lobby> CreateLobbyAsync(string hostUserId, string lobbyName, int playerLimit)
        {
            if (playerLimit < 2 || playerLimit > 6)
                throw new ArgumentException("Player limit must be between 2 and 6.");

            var lobby = new Lobby
            {
                HostUserId = hostUserId,
                Name = lobbyName,
                PlayerLimit = playerLimit
            };

            return await _lobbyRepository.CreateLobbyAsync(lobby);
        }

        public async Task<bool> JoinLobbyAsync(int lobbyId, string userId, string username)
        {
            var user = new User { Id = userId, Username = username };
            return await _lobbyRepository.AddPlayerToLobbyAsync(lobbyId, user);
        }

        public async Task<bool> LeaveLobbyAsync(int lobbyId, string userId)
        {
            var user = new User { Id = userId };
            return await _lobbyRepository.RemovePlayerFromLobbyAsync(lobbyId, user);
        }

        public async Task<IEnumerable<Lobby>> GetAllLobbiesAsync()
        {
            return await _lobbyRepository.GetAllLobbiesAsync();
        }

        public async Task<Lobby> GetLobbyByIdAsync(int lobbyId)
        {
            return await _lobbyRepository.GetLobbyByIdAsync(lobbyId);
        }

        public async Task<bool> StartGameAsync(int lobbyId)
        {
            var lobby = await _lobbyRepository.GetLobbyByIdAsync(lobbyId);
            if (lobby == null || lobby.IsGameStarted) return false;

            lobby.IsGameStarted = true;
            await _lobbyRepository.UpdateLobbyAsync(lobby);
            return true;
        }
    }
}
