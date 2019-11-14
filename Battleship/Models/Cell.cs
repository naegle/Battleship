using System;

namespace Battleship.Models
{
    public class Cell
    {
        public readonly static string HIT = "HIT";
        public readonly static string MISS = "MISS";
        public readonly static string NEITHER = "NEITHER";

        private string HitMissOrNeither { get; set; }
        private string PartialShip { get; set; }

        public Cell()
        {
            this.HitMissOrNeither = NEITHER;
            this.PartialShip = NEITHER;
        }

        public bool IsValidShipPlacement(string shipType, int column, int row, bool isVertical)
        {
            if (column > 9 || column < 0 || row > 9 || row < 0)
            {
                return false;
            }

            int shipLength;
            if (shipType.Equals(Ship.CARRIER))
                shipLength = 5;
            else if (shipType.Equals(Ship.BATTLESHIP))
                shipLength = 4;
            else if (shipType.Equals(Ship.CRUISER))
                shipLength = 3;
            else if (shipType.Equals(Ship.SUBMARINE))
                shipLength = 3;
            else if (shipType.Equals(Ship.DESTROYER))
                shipLength = 2;
            else throw new ArgumentException(shipType + " is not a valid ship type.");

            if (isVertical)
            {   
                return row + shipLength - 1 <= 9;
            }

            else
            {
                return column + shipLength - 1 <= 9;
            }
        }
    }
}