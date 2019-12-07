﻿/**
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
