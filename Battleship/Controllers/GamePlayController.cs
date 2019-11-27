using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class GamePlayController : Controller
    {
        private GamesService gameService;

        public GamePlayController(GamesService _gs)
        {
            gameService = _gs;
        }

        public IActionResult Index()
        {

            return View("GamePlayViewPage");
        }

        
        public IActionResult FireAIGrid(string coords)
        {
            gameService.NewGame();
            return View();
        }

        public IActionResult CreateGameService()
        {
            
            return View();
        }
    }
}