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


        private void calcuateAccuracyScore()
        {
            accuracyScore = (hitShots / totalShots) * 100;
        }

        public void incrementShot(bool hitOrMiss)
        {
            if (hitOrMiss)
            {
                hitShots += 1;
            }
            else
            {
                // miss shot
            }

            totalShots += 1;
        }

        public double getAccuracyScore()
        {
            this.calcuateAccuracyScore();

            return accuracyScore;
        }

        public void resetAccuracyScore()
        {
            hitShots = 0;
            totalShots = 0;
        }
    }
}
