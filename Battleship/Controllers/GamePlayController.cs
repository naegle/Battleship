﻿/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    The controller for the game view.
*/
using System;
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
        private readonly BattleshipDBContext battleshipContext;

        public GamePlayController(GamesService _gs, UserManager<IdentityUser> _userManager, BattleshipDBContext _battshipContext)
        {
            gameService = _gs;
            userManager = _userManager;
            battleshipContext = _battshipContext;
        }

        //The index method for the controller.
        public IActionResult Index()
        {
            string loggedUsername = userManager.GetUserName(User);

            var databaseUser = battleshipContext.Inventories.Where(i => i.PlayerId == loggedUsername).FirstOrDefault();

            // if empty
            if (databaseUser == null)
            {
                // then create one 
                CreateNewUserToDB(loggedUsername);

            }
            else
            {
                ViewData["Power1Count"] = databaseUser.Power1;
                ViewData["Power2Count"] = databaseUser.Power2;
                ViewData["Power3Count"] = databaseUser.Power3;
            }


            return View("GamePlayViewPage");
        }

        //This creates the action of firing at the AI grid.
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

                //if user win, convert their score to credit and update it into the user's DB table
                int creditIncome = Convert.ToInt32(score);
                updateUserCredit(creditIncome);

                return Json(new { success = true, resultText = hitOrMiss, score = score, credit = creditIncome });
            }
            return Json(new {success = true, resultText = hitOrMiss, COL = col, ROW = row });
        }

        //This creates the action of firing at the player grid.
        public async Task<JsonResult> FireAtPlayerGrid()
        {
            // the AIShoot metod should also return the x and y coords
            string username = userManager.GetUserName(User);
            string result = gameService.AIShoot(username);
          
            string[] temp = result.Split(" ");

            return Json(new { success = true, resultText = temp[0], col = temp[1], row = temp[2] });
        }

        //This creates a new game and places both players ships randomly.
        public async Task<JsonResult> CreateGameService(object item)
        {
            string username = userManager.GetUserName(User);

            gameService.NewGame(username);
            gameService.PlaceAIShipsRandomly(username);
            string playerGridStatus = gameService.PlacePlayerShipsRandomly(username);

            return Json(new { success = true, gridStatus = playerGridStatus});
        }

        //This calls and randomizes the rocket barrage shot on the gameboard.
        public async Task<JsonResult> RocketBarrage()
        {
            string username = userManager.GetUserName(User);

            //check if user have enough to use it
            var databaseUser = battleshipContext.Inventories.Where(i => i.PlayerId == username).FirstOrDefault();

            if (databaseUser.Power1 == 0)
            {
                return Json(new { success = false });
            }
            else
            {
                //  decrement use power count
                databaseUser.Power1 -= 1;
                battleshipContext.Update(databaseUser);
                battleshipContext.SaveChanges();

                string[] tenResults = gameService.RocketBarrage(username).Split(" ");

                return Json(new
                {
                    success = true,
                    resultText1 = tenResults[0],
                    col1 = tenResults[1],
                    row1 = tenResults[2],
                    resultText2 = tenResults[3],
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
                    power1Count = databaseUser.Power1
                });
            }
        }

        //Updates the users credit in the database.
        private void updateUserCredit(int _creditIncome)
        {
            // check if user exist to the database, if not add them to the db
            // check if user is in the db
            string loggedUsername = userManager.GetUserName(User);

            var databaseUser = battleshipContext.Inventories.Where(i => i.PlayerId == loggedUsername).FirstOrDefault();

            // if empty
            if (databaseUser == null)
            {
                // then create one 
                CreateNewUserToDB(loggedUsername,creditIncome: _creditIncome);
            }
            else
            {
                databaseUser.Cash += _creditIncome;
                battleshipContext.Update(databaseUser);
                battleshipContext.SaveChanges();
            }
        }

        //Generates new user in database with starting credit of 1000.
        private void CreateNewUserToDB(string user, int creditIncome = 1000)
        {
            Inventory tempInventory = new Inventory();
            tempInventory.PlayerId = user;
            tempInventory.Power1 = 0;
            tempInventory.Power2 = 0;
            tempInventory.Power3 = 0;
            tempInventory.Cash = creditIncome;

            battleshipContext.Add(tempInventory);
            battleshipContext.SaveChangesAsync();

            ViewData["Power1Count"] = 0;
            ViewData["Power2Count"] = 0;
            ViewData["Power3Count"] = 0;
        }
    }
}