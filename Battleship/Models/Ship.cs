using System;

namespace Battleship.Models
{
    public class Ship
    {
        public readonly static string CARRIER = "CARRIER";
        public readonly static string BATTLESHIP = "BATTLESHIP";
        public readonly static string CRUISER = "CRUISER";
        public readonly static string SUBMARINE = "SUBMARINE";
        public readonly static string DESTROYER = "DESTROYER";

        private string ShipType { get; set; }
        private int Length { get; set; }
        private int CurrentDamage { get; set; }

        public Ship(string shipType)
        {
            if (shipType.Equals(CARRIER))
                this.Length = 5;
            else if (shipType.Equals(BATTLESHIP))
                this.Length = 4;
            else if (shipType.Equals(CRUISER))
                this.Length = 3;
            else if (shipType.Equals(SUBMARINE))
                this.Length = 3;
            else if (shipType.Equals(DESTROYER))
                this.Length = 2;
            else throw new ArgumentException(shipType + " is not a valid ship type.");

            this.ShipType = shipType;
        }

        /// <summary>
        /// Damages the ship, and returns true if the ship is sunk.
        /// </summary>
        /// <returns></returns>
        public bool Hit()
        {
            CurrentDamage++;
            return IsSunk();
        }

        public bool IsSunk()
        {
            return CurrentDamage >= Length;
        }
    }
}