using System;
using System.Collections.Generic;

namespace Battleship.Models
{
    /// <summary>
    /// This will be the service that hosts the games. Because this will probably be a website that allows multiple games to be
    /// running at once, we can't just have one Game object, we need a service that can keep track of all of the games at once.
    /// </summary>
    public class GamesService
    {
        public Dictionary<int, Game> Games;
        public int GameCounter;

        public GamesService()
        {
            this.Games = new Dictionary<int, Game>();
            GameCounter = 0;
        }

        public int NewGame()
        {
            this.Games.Add(++GameCounter, new Game());
            return GameCounter;
        }

        public void RestartGame(int gameId)
        {
            Games[gameId].NewGame();
        }

        public void PlaceAIShips(int gameId)
        {
            Games[gameId].PlaceAIShips();
        }

        public void PlacePlayerShipsRandomly(int gameId)
        {
            Games[gameId].PlacePlayerShipsRandomly();
        }

        public void NewGame(int gameId)
        {
            Games[gameId].NewGame();
        }

        /// <summary>
        /// This will either return HIT, MISS, WINN, the name of the ship sunk, or NOT YOUR TURN
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string PlayerShoot(int gameId, int column, int row)
        {
            string resultOfShot = Games[gameId].PlayerShoot(column, row);
            return resultOfShot;
        }

        /// <summary>
        /// Returns a essage in the form of [result of shot] [column] [row]
        /// Result of shot is either "HIT", "MISS", "LOSE", or one of the names of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShoot(int gameId)
        {
            string resultOfShot = Games[gameId].AIShoot();
            return resultOfShot;
        }
    }
}