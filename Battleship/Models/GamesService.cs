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
        public Dictionary<string, Game> Games;
        public int GameCounter;

        public GamesService()
        {
            this.Games = new Dictionary<string, Game>();
            GameCounter = 0;
        }

        public int NewGame(string _playerName)
        {
            this.Games.Add(_playerName, new Game(_playerName));
            return GameCounter;
        }

        public void RestartGame(string playerID)
        {
            Games[playerID].NewGame();
        }

        public void PlaceAIShips(string playerID)
        {
            Games[playerID].PlaceAIShips();
        }

        public void PlacePlayerShipsRandomly(string playerID)
        {
            Games[playerID].PlacePlayerShipsRandomly();
        }

        /// <summary>
        /// This will either return HIT, MISS, WINN, the name of the ship sunk, or NOT YOUR TURN
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string PlayerShoot(string playerID, int column, int row)
        {
            string resultOfShot = Games[playerID].PlayerShoot(column, row);
            return resultOfShot;
        }

        /// <summary>
        /// Returns a message in the form of [result of shot] [column] [row]
        /// Result of shot is either "HIT", "MISS", "LOSE", or one of the names of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShoot(string playerID)
        {
            string resultOfShot = Games[playerID].AIShoot();
            return resultOfShot;
        }

        /// <summary>
        /// Return the Player accuracy score after winning the game
        /// </summary>
        /// <param name="playerUsername"></param>
        /// <returns></returns>
        public double GetAccuracyScore(string playerUsername)
        {
            return Games[playerUsername].Player.getAccuracyScore();
        }
    }
}