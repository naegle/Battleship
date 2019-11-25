using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class GamePlayController : Controller
    {
        public IActionResult Index()
        {
            
           // return View("GamePlayViewPage");
            return View("GamePlayViewPage");
        }
    }
}