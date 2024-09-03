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
    }
}
