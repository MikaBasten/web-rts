using Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DAL.DB;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class LobbyRepository : ILobbyRepository
    {
        private readonly ApplicationDbContext _context;

        public LobbyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lobby> CreateLobbyAsync(Lobby lobby)
        {
            _context.Lobbies.Add(lobby);
            await _context.SaveChangesAsync();
            return lobby;
        }

        public async Task<Lobby> GetLobbyByIdAsync(int lobbyId)
        {
            return await _context.Lobbies
                .Include(l => l.Players)
                .ThenInclude(p => p.User) // Include the User entity for each player
                .FirstOrDefaultAsync(l => l.Id == lobbyId);
        }

        public async Task<IEnumerable<Lobby>> GetAllLobbiesAsync()
        {
            return await _context.Lobbies
                .Include(l => l.Players)
                .ThenInclude(p => p.User) // Include the User entity for each player
                .ToListAsync();
        }

        public async Task<Lobby> UpdateLobbyAsync(Lobby lobby)
        {
            _context.Lobbies.Update(lobby);
            await _context.SaveChangesAsync();
            return lobby;
        }

        public async Task<bool> DeleteLobbyAsync(int lobbyId)
        {
            var lobby = await _context.Lobbies.FindAsync(lobbyId);
            if (lobby == null) return false;

            _context.Lobbies.Remove(lobby);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPlayerToLobbyAsync(int lobbyId, Player player)
        {
            var lobby = await _context.Lobbies
                .Include(l => l.Players) // Include related players
                .FirstOrDefaultAsync(l => l.Id == lobbyId);

            if (lobby == null || lobby.Players.Count >= lobby.PlayerLimit)
                return false; // Lobby is full or not found

            lobby.Players.Add(player);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePlayerFromLobbyAsync(int lobbyId, string username)
        {
            // Fetch the lobby with the specified ID and include players and their associated users.
            var lobby = await _context.Lobbies
                .Include(l => l.Players)
                .ThenInclude(p => p.User) // Ensure the User entity is loaded.
                .FirstOrDefaultAsync(l => l.Id == lobbyId);

            if (lobby == null)
                return false; // Lobby not found.

            // Find the player to remove using the username from the associated User entity.
            var playerToRemove = lobby.Players.FirstOrDefault(p => p.User.Username == username);
            if (playerToRemove == null)
                return false; // Player not found in the lobby.

            // Remove the player from the lobby.
            lobby.Players.Remove(playerToRemove);

            // Save changes to the database.
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ToggleReadyStatusAsync(int lobbyId, string username)
        {
            var lobby = await _context.Lobbies
                .Include(l => l.Players)
                    .ThenInclude(p => p.User) // Include the User object in the query
                .FirstOrDefaultAsync(l => l.Id == lobbyId);

            if (lobby == null)
                return false;

            // Find the player whose User.Username matches the provided username
            var player = lobby.Players.FirstOrDefault(p => p.User.Username == username);
            if (player == null)
                return false;

            player.IsReady = !player.IsReady; // Toggle the ready status
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
