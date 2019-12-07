/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    THe ship model.
*/
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

        public readonly static double TOTAL_SHIP_HEALTH = ShipLengths[CARRIER] + ShipLengths[BATTLESHIP] + ShipLengths[CRUISER] + ShipLengths[SUBMARINE] + ShipLengths[DESTROYER];

        public static readonly List<string> ShipTypes = new List<string>
        {
            CARRIER,
            BATTLESHIP,
            CRUISER,
            SUBMARINE,
            DESTROYER,
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

        //Boolean that checks to see if the current damage is greather than or equal to the length of ship.
        public bool IsSunk()
        {
            return CurrentDamage >= Length;
        }
    }
}