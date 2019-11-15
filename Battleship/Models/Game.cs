using System;

namespace Battleship.Models
{
    public class Game
    {
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
    }
}