using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    public class GameStoreController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly BattleshipDBContext battleshipContext;

        public GameStoreController(UserManager<IdentityUser> _userManager, BattleshipDBContext _battshipContext)
        {
            userManager = _userManager;
            battleshipContext = _battshipContext;
        }

        public IActionResult Index()
        {

            // check if user is in the db
            string loggedUsername = userManager.GetUserName(User);

            var databaseUser = battleshipContext.Inventories.Where(i => i.PlayerId == loggedUsername).FirstOrDefault();

            // if empty
            if (databaseUser == null)
            {
                // then create one 

                Inventory tempInventory = new Inventory();
                tempInventory.PlayerId = loggedUsername;
                tempInventory.Power1 = 0;
                tempInventory.Power2 = 0;
                tempInventory.Power3 = 0;
                tempInventory.Cash = 0;

                battleshipContext.Add(tempInventory);
                battleshipContext.SaveChangesAsync();

                ViewData["UserCredit"] = 0;
                ViewData["Power1Count"] = 0;
                ViewData["Power2Count"] = 0;
                ViewData["Power3Count"] = 0;


            }
            else
            {
                ViewData["UserCredit"] = databaseUser.Cash;
                ViewData["Power1Count"] = databaseUser.Power1;
                ViewData["Power2Count"] = databaseUser.Power2;
                ViewData["Power3Count"] = databaseUser.Power3;
            }


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