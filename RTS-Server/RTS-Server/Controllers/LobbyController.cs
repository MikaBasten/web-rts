using Microsoft.AspNetCore.Mvc;

namespace RTS_Server.Controllers
{
    using Core.IServices;
    using Core.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace RTS_Server.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class LobbiesController : ControllerBase
        {
            private readonly ILobbyService _lobbyService;

            public LobbiesController(ILobbyService lobbyService)
            {
                _lobbyService = lobbyService;
            }

            // POST: api/lobbies
            [HttpPost]
            public async Task<ActionResult<Lobby>> CreateLobby([FromQuery] string hostUserId, [FromQuery] string lobbyName, [FromQuery] int playerLimit)
            {
                try
                {
                    var lobby = await _lobbyService.CreateLobbyAsync(hostUserId, lobbyName, playerLimit);
                    return Ok(lobby);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            // POST: api/lobbies/{lobbyId}/join
            [HttpPost("{lobbyId}/join")]
            public async Task<ActionResult> JoinLobby(string lobbyId, [FromQuery] string userId, [FromQuery] string username)
            {
                var success = await _lobbyService.JoinLobbyAsync(lobbyId, userId, username);
                if (!success)
                    return BadRequest("Unable to join the lobby.");

                return Ok("Joined the lobby successfully.");
            }

            // POST: api/lobbies/{lobbyId}/leave
            [HttpPost("{lobbyId}/leave")]
            public async Task<ActionResult> LeaveLobby(string lobbyId, [FromQuery] string userId)
            {
                var success = await _lobbyService.LeaveLobbyAsync(lobbyId, userId);
                if (!success)
                    return BadRequest("Unable to leave the lobby.");

                return Ok("Left the lobby successfully.");
            }

            // GET: api/lobbies
            [HttpGet]
            public async Task<ActionResult<List<Lobby>>> GetAllLobbies()
            {
                var lobbies = await _lobbyService.GetAllLobbiesAsync();
                return Ok(lobbies);
            }

            // GET: api/lobbies/{lobbyId}
            [HttpGet("{lobbyId}")]
            public async Task<ActionResult<Lobby>> GetLobbyById(string lobbyId)
            {
                var lobby = await _lobbyService.GetLobbyByIdAsync(lobbyId);
                if (lobby == null)
                    return NotFound("Lobby not found.");

                return Ok(lobby);
            }

            // POST: api/lobbies/{lobbyId}/start
            [HttpPost("{lobbyId}/start")]
            public async Task<ActionResult> StartGame(string lobbyId)
            {
                var success = await _lobbyService.StartGameAsync(lobbyId);
                if (!success)
                    return BadRequest("Unable to start the game.");

                return Ok("Game started successfully.");
            }
        }
    }

}
