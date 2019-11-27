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

        public GamesService()
        {
            this.Games = new Dictionary<string, Game>();
        }

        public void NewGame(string userId)
        {
            if (Games.TryGetValue(userId, out Game game))
            {
                game.NewGame();
            }

            Games.Add(userId, new Game());
        }

        public void PlaceAIShips(string userId)
        {
            Games[userId].PlaceAIShipsRandomly();
        }

        public void PlacePlayerShipsRandomly(string userId)
        {
            Games[userId].PlacePlayerShipsRandomly();
        }

        /// <summary>
        /// Returns a string message in the form of "[result of shot][space][column][space][row]"
        /// Result of shot is either "HIT", "MISS", "WIN", or the name of the ship sunk
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string PlayerShoot(string userId, int column, int row)
        {
            string[] response = Games[userId].PlayerShoot(column, row).Split(new char[] { ' ' });
            string resultOfShot = response[0];
            int xCoordinate = Convert.ToInt32(resultOfShot[1] + 1);
            int yCoordinate = Convert.ToInt32(resultOfShot[2] + 1);
            return resultOfShot + " " + xCoordinate + " " + yCoordinate;
        }

        /// <summary>
        /// Returns a string message in the form of "[result of shot][space][column][space][row]"
        /// Result of shot is either "HIT", "MISS", "LOSE", or the name of the ship sunk
        /// </summary>
        /// <returns></returns>
        public string AIShoot(string userId)
        {
            string[] response = Games[userId].AIShoot().Split(new char[] { ' ' });
            string resultOfShot = response[0];
            int xCoordinate = Convert.ToInt32(resultOfShot[1] + 1);
            int yCoordinate = Convert.ToInt32(resultOfShot[2] + 1);
            return resultOfShot + " " + xCoordinate + " " + yCoordinate;
        }
    }
}