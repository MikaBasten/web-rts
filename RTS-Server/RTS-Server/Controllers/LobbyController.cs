using Microsoft.AspNetCore.Mvc;

namespace RTS_Server.Controllers
{
    using Core.IServices;
    using Core.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Security.Claims;

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

            // POST: api/lobbies/create
            [HttpPost("create")]
            [Authorize] // Require authentication
            public async Task<IActionResult> CreateLobbyAsync([FromQuery] string name, [FromQuery] int playerLimit)
            {
                var userName = User.Identity.Name; // Get user ID from JWT token
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Invalid token.");

                var lobby = await _lobbyService.CreateLobbyAsync(userName, name, playerLimit);
                if (lobby == null)
                    return BadRequest("Failed to create lobby.");

                // Automatically add the creator to the lobby
                var addedToLobby = await _lobbyService.JoinLobbyAsync(lobby.Id, userName);
                if (!addedToLobby)
                    return BadRequest("Failed to join the newly created lobby.");

                return Ok(lobby);
            }
        

            // POST: api/lobbies/join
            [HttpPost("join/{lobbyId}")]
            [Authorize]
            public async Task<IActionResult> JoinLobbyAsync(int lobbyId)
            {
                var userName = User.Identity.Name; // Get user ID from JWT token
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Invalid token.");

                var success = await _lobbyService.JoinLobbyAsync(lobbyId, userName);
                if (!success)
                    return BadRequest("Failed to join lobby.");

                return Ok("Successfully joined the lobby.");
            }

            // POST: api/lobbies/leave
            [HttpPost("leave/{lobbyId}")]
            [Authorize]
            public async Task<IActionResult> LeaveLobbyAsync(int lobbyId)
            {
                var userId = User.Identity.Name; // Get user ID from JWT token
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid token.");

                var success = await _lobbyService.LeaveLobbyAsync(lobbyId, int.Parse(userId));
                if (!success)
                    return BadRequest("Failed to leave lobby.");

                return Ok("Successfully left the lobby.");
            }

            // GET: api/lobbies
            [HttpGet]
            [Authorize]
            public async Task<IActionResult> GetAllLobbiesAsync()
            {
                var lobbies = await _lobbyService.GetAllLobbiesAsync();
                return Ok(lobbies);
            }

            // GET: api/lobbies/{lobbyId}
            [HttpGet("{lobbyId}")]
            [Authorize]
            public async Task<IActionResult> GetLobbyByIdAsync(int lobbyId)
            {
                var lobby = await _lobbyService.GetLobbyByIdAsync(lobbyId);
                if (lobby == null)
                    return NotFound("Lobby not found.");

                return Ok(lobby);
            }

            // POST: api/lobbies/start
            [HttpPost("start/{lobbyId}")]
            [Authorize]
            public async Task<IActionResult> StartGameAsync(int lobbyId)
            {
                var userId = User.Identity.Name; // Get user ID from JWT token
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Invalid token.");

                var success = await _lobbyService.StartGameAsync(lobbyId);
                if (!success)
                    return BadRequest("Failed to start game.");

                return Ok("Game started successfully.");
            }
        }
    }

}
