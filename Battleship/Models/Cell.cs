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