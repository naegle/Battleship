﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class TutorialController : Controller
    {
        public IActionResult Index()
        {
            return View("HowToPlayIndex");
        }
    }
}