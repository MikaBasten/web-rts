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
                .Include(l => l.Players) // Include related players
                .FirstOrDefaultAsync(l => l.Id == lobbyId);
        }

        public async Task<IEnumerable<Lobby>> GetAllLobbiesAsync()
        {
            return await _context.Lobbies
                .Include(l => l.Players) // Include related players
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

        public async Task<bool> RemovePlayerFromLobbyAsync(int lobbyId, int userId)
        {
            var lobby = await _context.Lobbies
                .Include(l => l.Players) // Include related players
                .FirstOrDefaultAsync(l => l.Id == lobbyId);

            if (lobby == null) return false;

            var player = lobby.Players.FirstOrDefault(p => p.Id == userId);
            if (player == null) return false;

            lobby.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
