/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    The database set up for high scores and user inventory.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Models
{
    public class BattleshipDBContext : DbContext
    {
        public BattleshipDBContext(DbContextOptions<BattleshipDBContext> options)
            : base(options)
        { }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<HighScore> HighScores { get; set; }
    }

    public class Inventory { 
        [Key]
        public string PlayerId { get; set; }

        public int Power1 { get; set; }

        public int Power2 { get; set; }

        public int Power3 { get; set; }

        public int Cash { get; set; }
    }

    public class HighScore { 
    
        [Key]
        public string PlayerId { get; set; }

        public double AccuracyScore { get; set; }

        public DateTime Date_Of_Win { get; set; }
    }


}
