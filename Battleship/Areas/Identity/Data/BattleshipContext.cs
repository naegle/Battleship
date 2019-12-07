/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    The Identity login Database Context for the website.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Models
{
    public class BattleshipContext : IdentityDbContext<IdentityUser>
    {
        public BattleshipContext(DbContextOptions<BattleshipContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
