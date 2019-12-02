using System;

namespace Battleship.Models
{
    public class Game
    {
        Random random;

        public bool GameStarted;
        public bool YourTurn;

        public Grid PlayerGrid;
        public Grid AIGrid;

        public int PlayerShots;

        public int PlayerShipsRemaining;
        public int AIShipsRemaining;

        public Player Player;

        public Game(string _playerName)
        {
            this.GameStarted = true;
            this.YourTurn = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player = new Player(_playerName);

            random = new Random();
        }

        public void PlaceAIShips()
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
            this.YourTurn = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player.resetAccuracyScore();

            AIGrid.WipeGrid();
            AIGrid.PlaceShipsRandomly();
        }

        /// <summary>
        /// This will either return HIT, MISS, WIN, the name of the ship sunk, or NOT YOUR TURN
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string PlayerShoot(int column, int row)
        {
            if (!YourTurn)
            {
                return "NOT YOUR TURN";
            }

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
        /// Result of shot is either "HIT", "MISS", "LOSE", or one of the names of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShoot()
        {
            int columnShot = random.Next(10);
            int rowShot = random.Next(10);
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