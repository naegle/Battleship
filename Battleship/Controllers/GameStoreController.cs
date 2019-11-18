using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class GameStoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PurchasePower1()
        {
            return View();
        }

        public IActionResult PurchasePower2()
        {
            return View();
        }

        public IActionResult PurchasePower3()
        {
            return View();
        }

        public IActionResult PurchasePower4()
        {
            return View();
        }
    }
}