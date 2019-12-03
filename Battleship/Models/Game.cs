using System;
using System.Collections.Generic;
using System.Drawing;

namespace Battleship.Models
{
    public class Game
    {
        readonly Random Random;

        public bool GameStarted;
        public bool YourTurn;

        public Grid PlayerGrid;
        public Grid AIGrid;

        public double PlayerShots;

        public int PlayerShipsRemaining;
        public int AIShipsRemaining;

        public Player Player;

        public Game(string _playerName)
        public List<Point> SmartShots;

        public Game()
        {
            this.GameStarted = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player = new Player(_playerName);

            Random = new Random();

            SmartShots = CreateSmartShotsList();
        }

        public void PlaceAIShipsRandomly()
        {
            AIGrid.PlaceShipsRandomly();
        }

        public void PlacePlayerShipsRandomly()
        {
            PlayerGrid.PlaceShipsRandomly();
        }

        public void NewGame()
        {
            this.GameStarted = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player.resetAccuracyScore();

            AIGrid.WipeGrid();
            AIGrid.PlaceShipsRandomly();
        }

        /// <summary>
        /// Returns a string message in the form of "[result of shot] [column] [row] [accuracy(if you won on this shot)]"
        /// Result of shot is either "HIT", "MISS", "WIN", or the name of the ship you just sunk
        /// This will either return HIT, MISS, WIN, the name of the ship sunk, or NOT YOUR TURN
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string PlayerShoot(int column, int row)
        {
            string resultOfShot = AIGrid.ShootCell(column, row);

            if (resultOfShot.Equals("HIT"))
            {
                PlayerShots++;
                this.Player.incrementShot(true);
            }
            else if (resultOfShot.Equals("MISS")){
                this.Player.incrementShot(false);
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                AIShipsRemaining--;
                this.Player.incrementShot(true);
                if (AIShipsRemaining == 0)
                {
                    return "WIN" + " " + column + " " + row + " " + (Ship.TOTAL_SHIP_HEALTH * 100) / PlayerShots;
                }
            }

            return resultOfShot + " " + column + " " + row;
        }

        /// <summary>
        /// Returns a essage in the form of [result of shot] [column] [row]
        /// Result of shot is either "HIT", "MISS", "LOSE", or the name of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShootDumb()
        {
            int columnShot = Random.Next(10);
            int rowShot = Random.Next(10);
            string resultOfShot = PlayerGrid.ShootCell(columnShot, rowShot);
            if (resultOfShot.Equals("DUPLICATE"))
            {
                resultOfShot = AIShootDumb();
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                PlayerShipsRemaining--;
                if (PlayerShipsRemaining == 0)
                {
                    return "Lose " + columnShot + " " + rowShot;
                }
            }

            return resultOfShot + " " + columnShot + " " + rowShot;
        }

        /// <summary>
        /// Returns a essage in the form of [result of shot] [column] [row]
        /// Result of shot is either "HIT", "MISS", "LOSE", or the name of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShootSmart()
        {
            //THIS METHOD IS NOT DONE!

            Point smartShot = SmartShots[0];
            int columnShot = smartShot.X;
            int rowShot = smartShot.Y;
            SmartShots.Remove(smartShot);
            string resultOfShot = PlayerGrid.ShootCell(columnShot, rowShot);
            if (resultOfShot.Equals("DUPLICATE"))
            {
                resultOfShot = AIShootDumb();
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                PlayerShipsRemaining--;
                if (PlayerShipsRemaining == 0)
                {
                    return "Lose " + columnShot + " " + rowShot;
                }
            }

            return resultOfShot + " " + columnShot + " " + rowShot;
        }

        public List<Point> CreateSmartShotsList()
        {
            List<Point> shots = new List<Point>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    shots.Add(new Point(i, j));
                }
            }

            ShuffleTheList(shots);
            
            return shots;
        }

        /// <summary>
        /// Shuffle algorithm taken from https://stackoverflow.com/questions/273313/randomize-a-listt
        /// </summary>
        /// <param name="list"></param>
        public void ShuffleTheList(List<Point> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Point value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}