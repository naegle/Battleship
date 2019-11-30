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


        public async Task<JsonResult> FireAIGrid(string coords)
        {
            var temp = coords.Split('_');

            int col = Int32.Parse(temp[0]);
            int row = Int32.Parse(temp[1]);


           string hitOrMiss = gameService.PlayerShoot(1,col,row);
            return Json(new {success = true, resultText = hitOrMiss });
        }

        public async Task<JsonResult> FireAtPlayerGrid()
        {
            // the AIShoot metod should also return the x and y coords
            string result = gameService.AIShoot(1);

            string[] temp = result.Split(" ");

            return Json(new { success = true, resultText = temp[0], col = temp[1], row = temp[2] });
        }

        public async Task<JsonResult> CreateGameService()
        {
            gameService.NewGame();
            gameService.PlaceAIShips(1);
            gameService.PlacePlayerShipsRandomly(1);

            return Json(new { success = true });
        }

        public async Task<JsonResult> ResetGame()
        {
            return null;
        }


        public async Task<JsonResult> WinGame()
        {
            return null;
        }

    }
}