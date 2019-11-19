using Battleship.Models;
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

        }
    }
}
