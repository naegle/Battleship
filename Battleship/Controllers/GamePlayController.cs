using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class GamePlayController : Controller
    {
        private GamesService gameService;
        private readonly UserManager<IdentityUser> userManager;

        public GamePlayController(GamesService _gs, UserManager<IdentityUser> _userManager)
        {
            gameService = _gs;
            userManager = _userManager;
        }

        public IActionResult Index()
        {

            return View("GamePlayViewPage");
        }


        public async Task<JsonResult> FireAIGrid(string coords)
        {
            string username = userManager.GetUserName(User);

            var temp = coords.Split('_');

            int col = Int32.Parse(temp[0]);
            int row = Int32.Parse(temp[1]);


           string hitOrMiss = gameService.PlayerShoot(username, col,row);

            // get calucate score
            if (hitOrMiss == "WIN")
            {
                string _score = gameService.GetAccuracyScore(username).ToString("#.###");

                return Json(new { success = true, resultText = hitOrMiss, score = _score });

            }

            return Json(new {success = true, resultText = hitOrMiss });
        }

        public async Task<JsonResult> FireAtPlayerGrid()
        {
            // the AIShoot metod should also return the x and y coords
            string username = userManager.GetUserName(User);
            string result = gameService.AIShoot(username);

            string[] temp = result.Split(" ");

            return Json(new { success = true, resultText = temp[0], col = temp[1], row = temp[2] });
        }

        public async Task<JsonResult> CreateGameService()
        {
            string username = userManager.GetUserName(User);

            gameService.NewGame(username);
            gameService.PlaceAIShipsRandomly(username);
            gameService.PlacePlayerShipsRandomly(username);

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