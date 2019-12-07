/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    Controls the requests and responses for the high scores page.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class HighScoreController : Controller
    {
        private readonly BattleshipDBContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public HighScoreController(BattleshipDBContext context, UserManager<IdentityUser> _userManager)
        {
            _context = context;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
           var listOfHighScore =  _context.HighScores.OrderByDescending(x => x.AccuracyScore).Take(25);
            
            return View("HighScoreIndex", listOfHighScore);
        }

        //Adds high score to the database and then updates the page.
        public async Task<JsonResult> AddHighScore(float score)
        {
            string username = userManager.GetUserName(User);
            HighScore highScore = new HighScore();
            highScore.PlayerId = username + DateTime.Now.ToString();
            highScore.AccuracyScore = score;
            highScore.Date_Of_Win = DateTime.Now;

            _context.HighScores.Add(highScore);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}