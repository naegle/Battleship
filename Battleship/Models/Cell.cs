using System;

namespace Battleship.Models
{
    public class Cell
    {
        public readonly static string HIT = "HIT";
        public readonly static string MISS = "MISS";
        public readonly static string NONE = "NONE";

        public string HitMissOrNone { get; set; }
        public string PartialShip { get; set; }

        public Cell()
        {
            this.HitMissOrNone = NONE;
            this.PartialShip = NONE;
        }
    }
}