using System;
using System.Collections.Generic;

namespace Battleship.Models
{
    public class Ship
    {
        public readonly static string CARRIER = "CARRIER";
        public readonly static string BATTLESHIP = "BATTLESHIP";
        public readonly static string CRUISER = "CRUISER";
        public readonly static string SUBMARINE = "SUBMARINE";
        public readonly static string DESTROYER = "DESTROYER";

        public static readonly Dictionary<string, int> ShipLengths = new Dictionary<string, int>
        {
            { CARRIER, 5 },
            { BATTLESHIP, 4 },
            { CRUISER, 3 },
            { SUBMARINE, 3 },
            { DESTROYER, 2 },
        };

        private string ShipType { get; set; }
        private int Length { get; set; }
        private int CurrentDamage { get; set; }

        public Ship(string shipType)
        {
            this.ShipType = shipType;

            if (ShipLengths.TryGetValue(shipType, out int length))
            {
                this.Length = length;
            }

            else throw new ArgumentException(shipType + " is not a valid ship type.");

            this.CurrentDamage = 0;
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