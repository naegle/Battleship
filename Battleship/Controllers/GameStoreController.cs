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
                tempInventory.Cash = 1000;

                battleshipContext.Add(tempInventory);
                battleshipContext.SaveChangesAsync();

                ViewData["UserCredit"] = tempInventory.Cash;
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

        public async Task<JsonResult> PurchasePowerUps(string powerupID)
        {
            string loggedUsername = userManager.GetUserName(User);

            Inventory databaseUser = battleshipContext.Inventories.Where(i => i.PlayerId == loggedUsername).FirstOrDefault();
            

            if (powerupID == "powerup1")
            {
                return  CaluclatePurchase(databaseUser, 10, powerupID);
            }
            else if (powerupID == "powerup2")
            {
                return  CaluclatePurchase(databaseUser, 50, powerupID);
            }
            else
            {
                return  CaluclatePurchase(databaseUser, 100, powerupID);
            }

        }

        private JsonResult CaluclatePurchase(Inventory user, int cost, string powerupType)
        {

            // Check if user have enough credit to make a purchase
            if (user.Cash < cost)
            {
                return Json(new { success = true, resultText = "NOT ENOUGH" });
            }

            //  At this point they got enough credit to make purchase
            int calculatePurchase = user.Cash - cost;

            // increase quantity count
            // update user cash
            if (powerupType == "powerup1")
            {
                user.Power1++;
                user.Cash = calculatePurchase;

                battleshipContext.Update(user);
                battleshipContext.SaveChanges();

                return Json(new { success = true, resultText = "PURCHASED_POWER1", powerupCount = user.Power1, userCredit = user.Cash });
            }
            else if (powerupType == "powerup2")
            {
                user.Power2++;
                user.Cash = calculatePurchase;

                battleshipContext.Update(user);
                battleshipContext.SaveChanges();

                return Json(new { success = true, resultText = "PURCHASED_POWER2", powerupCount = user.Power2, userCredit = user.Cash });
            }
            else
            {
                user.Power3++;
                user.Cash = calculatePurchase;

                battleshipContext.Update(user);
                battleshipContext.SaveChanges();

                return Json(new { success = true, resultText = "PURCHASED_POWER3", powerupCount = user.Power3, userCredit = user.Cash });
            }
            
        }


    }
}