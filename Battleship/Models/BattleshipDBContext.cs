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
