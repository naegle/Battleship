/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    Represents the player class and all of its functionality.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Player
    {
        private string username { get; set; }
        private DateTime dateOfVictory { get; set; }
        private double accuracyScore;
        private double totalShots;
        private double hitShots;

        public  Player(string name)
        {
            this.username = name;
            this.accuracyScore = 0.0;
            this.totalShots = 0.0;
            this.hitShots = 0.0;
        }

        private void CalculateAccuracyScore()
        {
            accuracyScore = (hitShots / totalShots) * 100;
        }

        //Increments the shot
        public void IncrementShot(bool hitOrMiss)
        {
            if (hitOrMiss)
            {
                hitShots += 1;
            }

            totalShots += 1;
        }

        //Gets the player's accuracy score
        public double GetAccuracyScore()
        {
            this.CalculateAccuracyScore();

            return accuracyScore;
        }

        //Resets player's accuracy score.
        public void ResetAccuracyScore()
        {
            hitShots = 0;
            totalShots = 0;
        }
    }
}
