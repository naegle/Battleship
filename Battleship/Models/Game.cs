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

        public int PlayerShipsRemaining;
        public int AIShipsRemaining;

        public Player Player;

        public Game()
        {
            this.GameStarted = false;
            this.YourTurn = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            this.Player = new Player("Anonymous");

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
            this.GameStarted = false;
            this.YourTurn = true;
            this.PlayerGrid = new Grid();
            this.AIGrid = new Grid();

            this.PlayerShipsRemaining = 5;
            this.AIShipsRemaining = 5;

            AIGrid.WipeGrid();
            AIGrid.PlaceShipsRandomly();
        }

        public string PlayerShoot(int column, int row)
        {
            string resultOfShot = AIGrid.ShootCell(column, row);


            if (resultOfShot.Equals("HIT"))
            {
                //increment player shots and player hits
            }

            if (resultOfShot.Equals("MISS"))
            {
                //increment player shots
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                AIShipsRemaining--;
                if (AIShipsRemaining == 0)
                {
                    return "You Win";
                }
            }

            return resultOfShot;
        }

        public string AIShoot()
        {
            string resultOfShot = PlayerGrid.ShootCell(random.Next(0, 9), random.Next(0, 9));
            if (resultOfShot.Equals("DUPLICATE")) //TODO: make this smart instead of totally random
            {
                resultOfShot = AIShoot();
            }

            if (Ship.ShipTypes.Contains(resultOfShot))
            {
                PlayerShipsRemaining--;
                if (PlayerShipsRemaining == 0)
                {
                    return "You Lose";
                }
            }

            return resultOfShot;
        }
    }
}