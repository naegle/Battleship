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

        /// <summary>
        /// For index, we can simply return the top 25 players?
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
           var listOfHighScore =  _context.HighScores.OrderByDescending(x => x.AccuracyScore).Take(25);
            
            return View("HighScoreIndex", listOfHighScore);
        }

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