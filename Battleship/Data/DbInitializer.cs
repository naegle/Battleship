/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    Initializes both databases.
*/
using Battleship.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Data
{
    public class DbInitializer
    {
        public static void Initialize(BattleshipDBContext context, BattleshipContext userContext)
        {
            context.Database.Migrate();
            userContext.Database.Migrate();

            //  add register users in UsersRolesDB
            addUsers(userContext);

            //  add roles in UserRolesDB
            addUserInventory(context);

            // add random names to the HighScore
            addUserHighScore(context);
        }


        /// <summary>
        /// This method task is to initalize data for the User table of the
        /// Identity model
        /// </summary>
        /// <param name="context"></param>
        private static void addUsers(BattleshipContext context)
        {

            if (context.Users.Any())
            {
                var delete =
                    from u in context.Users
                    select u;

                foreach (IdentityUser use in delete)
                {
                    context.Users.Remove(use);
                }

                context.SaveChanges();
            }
            //  JohnDoe password is Tom1996! for testing
            var users = new IdentityUser[]
            {
                

                new IdentityUser
                { Id ="54b09006-6d89-422d-bd89-a902f309acbe", UserName ="JohnDoe",
                    NormalizedUserName ="JOHNDOE",
                    Email ="123@123.com" ,
                    NormalizedEmail = "123@123.COM",
                    EmailConfirmed = true,
                    PasswordHash ="AQAAAAEAACcQAAAAEE+cuefISYzXCw85lprijJVHu/13nHLOXBPncGdlcbfMhlcZ3vzwG/ixvxK0Wk59xg==",
                    SecurityStamp ="GSS2ESOSS2AFFOZVJXBDBNT3QN7YLBL6",
                    ConcurrencyStamp ="020bf919-cff4-442b-9e67-ff5a65766cbd"

                }

            };

            foreach (IdentityUser iu in users)
            {
                context.Users.Add(iu);
            }

            context.SaveChanges();
        }


        /// <summary>
        /// This mehthod task is to intialzie the Inventory Table 
        /// </summary>
        /// <param name="context"></param>
        private static void addUserInventory(BattleshipDBContext context)
        {
            //  If there is any data in inventory table,
            //  remove the data
            if (context.Inventories.Any())
            {
                var delete =
                    from u in context.Inventories
                    select u;

                foreach (Inventory use in delete)
                {
                    context.Inventories.Remove(use);
                }

                context.SaveChanges();
            }

            
            var usersRoles = new Inventory[]
            {
                new Inventory {PlayerId = "JohnDoe", Power1 = 0, Power2 = 0, Power3 = 0, Cash = 10000}
            };

            foreach (Inventory iru in usersRoles)
            {
                context.Inventories.Add(iru);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// This mehthod task is to intialzie the Inventory Table 
        /// </summary>
        /// <param name="context"></param>
        private static void addUserHighScore(BattleshipDBContext context)
        {
            //  If there is any data in inventory table,
            //  remove the data
            if (context.HighScores.Any())
            {
                var delete =
                    from u in context.HighScores
                    select u;

                foreach (HighScore use in delete)
                {
                    context.HighScores.Remove(use);
                }

                context.SaveChanges();
            }


            var highScore = new HighScore[]
            {
                new HighScore {PlayerId = "Jim", AccuracyScore = 100.0, Date_Of_Win = DateTime.Today},
                new HighScore {PlayerId = "Eric", AccuracyScore = 99.9, Date_Of_Win = DateTime.Today},
                new HighScore {PlayerId = "Tom", AccuracyScore = 25.0, Date_Of_Win = DateTime.Today},
                new HighScore {PlayerId = "Chris", AccuracyScore = 75.0, Date_Of_Win = DateTime.Today},

            };

            foreach (HighScore score in highScore)
            {
                context.HighScores.Add(score);
            }

            context.SaveChanges();
        }
    }
}
