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

        
        public List<Point> SmartShots;
        public List<Point> DamageTrackingShots;

        public Game(string _playerName)
        {
            this.GameStarted = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player = new Player(_playerName);

            Random = new Random();

            SmartShots = CreateSmartShotsList();
            DamageTrackingShots = new List<Point>();
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

            SmartShots = CreateSmartShotsList();
            DamageTrackingShots.Clear();
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
                    return "WIN";
                }
            }

            return resultOfShot;
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
                    return "LOSE " + columnShot + " " + rowShot;
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
            Point shot;

            if (DamageTrackingShots.Count > 0)
            {
                shot = DamageTrackingShots[0];
            }
            else if (SmartShots.Count > 0)
            {
                shot = SmartShots[0];
            }
            else return AIShootDumb();

            int column = shot.X;
            int row = shot.Y;
            string resultOfShot = PlayerGrid.ShootCell(column, row);
            if (resultOfShot.Equals("DUPLICATE"))
            {
                resultOfShot = AIShootDumb();
            }
            if (resultOfShot.Equals("HIT"))
            {
                AddNeighborCellsToList(DamageTrackingShots, new Point(column, row));
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                DamageTrackingShots.Clear();
                RemoveNeighborCellsFromList(SmartShots, new Point(column, row));
                PlayerShipsRemaining--;
                if (PlayerShipsRemaining == 0)
                {
                    return "Lose " + column + " " + row;
                }
            }

            if (SmartShots.Contains(new Point(column, row)))
            {
                SmartShots.Remove(new Point(column, row));
            }
            if (DamageTrackingShots.Contains(new Point(column, row)))
            {
                DamageTrackingShots.Remove(new Point(column, row));
            }

            return resultOfShot + " " + column + " " + row;
        }

        /// <summary>
        /// This method adds shots in a checkerboard fashion. It's smarter and more efficient to check spots if you start out by not checking spots that are right next to you.
        /// </summary>
        /// <returns></returns>
        public List<Point> CreateSmartShotsList()
        {
            List<Point> shots = new List<Point>();
            for (int i = 0; i < 10; i=i+2)
            {
                for (int j = 0; j < 10; j=j+2)
                {
                    shots.Add(new Point(i, j));
                }
            }

            for (int i = 1; i < 10; i = i + 2)
            {
                for (int j = 1; j < 10; j = j + 2)
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

        private void RemoveNeighborCellsFromList(List<Point> list, Point point)
        {
            if (list.Contains(new Point(point.X + 1, point.Y)))
            {
                list.Remove(new Point(point.X + 1, point.Y));
            }

            if (list.Contains(new Point(point.X - 1, point.Y)))
            {
                list.Remove(new Point(point.X - 1, point.Y));
            }

            if (list.Contains(new Point(point.X, point.Y + 1)))
            {
                list.Remove(new Point(point.X, point.Y + 1));
            }

            if (list.Contains(new Point(point.X, point.Y - 1)))
            {
                list.Remove(new Point(point.X, point.Y - 1));
            }
        }

        private void AddNeighborCellsToList(List<Point> list, Point point)
        {
            var testGrid = PlayerGrid.Cells[point.X, point.Y];
            var testStatus = PlayerGrid.Cells[point.X, point.Y].HitMissOrNone;
            if (!list.Contains(new Point(point.X + 1, point.Y)) && PointIsWithinTheGrid(new Point(point.X + 1, point.Y)) && PlayerGrid.Cells[point.X + 1, point.Y].HitMissOrNone.Equals("NONE"))
            {
                list.Add(new Point(point.X + 1, point.Y));
            }

            if (!list.Contains(new Point(point.X - 1, point.Y)) && PointIsWithinTheGrid(new Point(point.X - 1, point.Y)) && PlayerGrid.Cells[point.X - 1, point.Y].HitMissOrNone.Equals("NONE"))
            {
                list.Add(new Point(point.X - 1, point.Y));
            }

            if (!list.Contains(new Point(point.X, point.Y + 1)) && PointIsWithinTheGrid(new Point(point.X, point.Y + 1)) && PlayerGrid.Cells[point.X, point.Y + 1].HitMissOrNone.Equals("NONE"))
            {
                list.Add(new Point(point.X, point.Y + 1));
            }

            if (!list.Contains(new Point(point.X, point.Y - 1)) && PointIsWithinTheGrid(new Point(point.X, point.Y - 1)) && PlayerGrid.Cells[point.X, point.Y - 1].HitMissOrNone.Equals("NONE"))
            {
                list.Add(new Point(point.X, point.Y - 1));
            }
        }

        private bool PointIsWithinTheGrid(Point point)
        {
            bool test = point.X >= 0 && point.X < 10 && point.Y >= 0 && point.Y < 10;
            return test;
        }

        /// <summary>
        /// Rocket Baragges hit random squares, and they can hit squares that have already been hit.
        /// </summary>
        /// <returns></returns>
        public string RocketBarrage()
        {
            string totalResult = "";
            HashSet<Point> points = new HashSet<Point>();

            while (points.Count < 10)
            {
                points.Add(new Point(Random.Next(10), Random.Next(10)));
            }

            foreach (Point point in points)
            {
                string resultOfShot = AIGrid.ShootCell(point.X, point.Y);
                if (resultOfShot.Equals("DUPLICATE"))
                {
                    resultOfShot = AIGrid.Cells[point.X, point.Y].HitMissOrNone;
                }

                if (Ship.ShipTypes.Contains(resultOfShot))
                {
                    AIShipsRemaining--;
                    if (AIShipsRemaining == 0)
                    {
                        totalResult += "WIN " + point.X + " " + point.Y + " ";
                    }
                }

                else totalResult += resultOfShot + " " + point.X + " " + point.Y + " ";

                if (resultOfShot.Equals(Cell.HIT))
                {
                    this.Player.incrementShot(true);
                }

                if (resultOfShot.Equals(Cell.MISS))
                {
                    this.Player.incrementShot(false);
                }
            }

            PlayerShots += 10;

            return totalResult;
        }
    }
}