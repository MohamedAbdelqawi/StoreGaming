using GamePlay.Models;
using GamePlay.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamePlay.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _GamesService;

        public HomeController(IGamesService GamesService)
        {
            _GamesService = GamesService;
        }

        public IActionResult Index()
        {
            var games = _GamesService.GetAll();
            return View(games);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
