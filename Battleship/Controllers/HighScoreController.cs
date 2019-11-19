using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class HighScoreController : Controller
    {
        private readonly BattleshipDBContext _context;

        public HighScoreController(BattleshipDBContext context)
        {
            _context = context;
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
    }
}