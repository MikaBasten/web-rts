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
        }
    }

}
