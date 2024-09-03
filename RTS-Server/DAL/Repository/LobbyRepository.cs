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
    }
}
