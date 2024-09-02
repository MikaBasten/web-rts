using Microsoft.AspNetCore.Mvc;

namespace RTS_Server.Controllers
{
    public class LobbyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
