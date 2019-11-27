using System;

namespace Battleship.Models
{
    public class Game
    {
        readonly Random Random;

        public bool GameStarted;
        public bool YourTurn;

        public Grid PlayerGrid;
        public Grid AIGrid;

        public int PlayerShots;

        public int PlayerShipsRemaining;
        public int AIShipsRemaining;

        public Player Player;

        public Game()
        {
            this.GameStarted = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player = new Player("Anonymous"); //TODO: Make this work if necessary

            Random = new Random();
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

            AIGrid.PlaceShipsRandomly();
        }

        /// <summary>
        /// This will either return HIT, MISS, the name of the ship sunk, or WIN
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
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                AIShipsRemaining--;
                if (AIShipsRemaining == 0)
                {
                    return "WIN" + " " + column + " " + row;
                }
            }

            return resultOfShot + " " + column + " " + row;
        }

        /// <summary>
        /// Returns a essage in the form of [result of shot] [column] [row]
        /// Result of shot is either "HIT", "MISS", "LOSE", or the name of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShoot()
        {
            int columnShot = Random.Next(10);
            int rowShot = Random.Next(10);
            string resultOfShot = PlayerGrid.ShootCell(columnShot, rowShot);
            if (resultOfShot.Equals("DUPLICATE")) //TODO: make this smart instead of totally random
            {
                resultOfShot = AIShoot();
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
    }
}