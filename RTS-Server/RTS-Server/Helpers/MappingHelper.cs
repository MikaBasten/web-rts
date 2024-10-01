using Core.Models;
using RTS_Server.Dtos;

namespace RTS_Server.Helpers
{
    public static class MappingHelper
    {
        // Converts a Lobby entity to a LobbyDto
        public static LobbyDto ToLobbyDto(Lobby lobby)
        {
            return new LobbyDto
            {
                Id = lobby.Id,
                Name = lobby.Name,
                PlayerLimit = lobby.PlayerLimit,
                Players = lobby.Players?.Select(p => ToPlayerDto(p)).ToList() // Convert players to PlayerDto
            };
        }

        // Converts a Player entity to a PlayerDto
        public static PlayerDto ToPlayerDto(Player player)
        {
            return new PlayerDto
            {
                Username = player.User?.Username, // Ensure User is not null
                IsReady = player.IsReady,
                SelectedFaction = player.SelectedFaction
            };
        }

        // Converts a list of Lobby entities to a list of LobbyDto
        public static List<LobbyDto> ToLobbyDtoList(IEnumerable<Lobby> lobbies)
        {
            return lobbies.Select(l => ToLobbyDto(l)).ToList();
        }
    }
}
