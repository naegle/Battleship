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

            int col = Int32.Parse(temp[1]);
            int row = Int32.Parse(temp[2]);


           string hitOrMiss = gameService.PlayerShoot(username, col,row);

            // get calucate score
            if (hitOrMiss.ToUpper().Contains("WIN"))
            {
                //string _score = gameService.GetAccuracyScore(username).ToString("#.###");
                float score = Convert.ToUInt64(Math.Round(Convert.ToDouble(hitOrMiss.Split(' ')[1])));

                return Json(new { success = true, resultText = hitOrMiss, score = score });

            }

            return Json(new {success = true, resultText = hitOrMiss, COL = col, ROW = row });
        }

        public async Task<JsonResult> FireAtPlayerGrid()
        {
            // the AIShoot metod should also return the x and y coords
            string username = userManager.GetUserName(User);
            string result = gameService.AIShoot(username);
          
            string[] temp = result.Split(" ");

            return Json(new { success = true, resultText = temp[0], col = temp[1], row = temp[2] });
        }

        public async Task<JsonResult> CreateGameService(object item)
        {
            string username = userManager.GetUserName(User);

            gameService.NewGame(username);
            gameService.PlaceAIShipsRandomly(username);
            string playerGridStatus = gameService.PlacePlayerShipsRandomly(username);

            return Json(new { success = true, gridStatus = playerGridStatus});
        }

        public async Task<JsonResult> RocketBarrage()
        {
            string username = userManager.GetUserName(User);
            string[] tenResults = gameService.RocketBarrage(username).Split(" ");


            return Json(new { success = true,
                resultText1 = tenResults[0],
                col1 = tenResults[1],
                row1 = tenResults[2],
                resultText2= tenResults[3],
                col2 = tenResults[4],
                row2 = tenResults[5],
                resultText3 = tenResults[6],
                col3 = tenResults[7],
                row3 = tenResults[8],
                resultText4 = tenResults[9],
                col4 = tenResults[10],
                row4 = tenResults[11],
                resultText5 = tenResults[12],
                col5 = tenResults[13],
                row5 = tenResults[14],
                resultText6 = tenResults[15],
                col6 = tenResults[16],
                row6 = tenResults[17],
                resultText7 = tenResults[18],
                col7 = tenResults[19],
                row7 = tenResults[20],
                resultText8 = tenResults[21],
                col8 = tenResults[22],
                row8 = tenResults[23],
                resultText9 = tenResults[24],
                col9 = tenResults[25],
                row9 = tenResults[26],
                resultText10 = tenResults[27],
                col10 = tenResults[28],
                row10 = tenResults[29],
            });
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