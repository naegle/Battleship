/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    Service handler for the game.
*/
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

        //Creates a new game and stores it to a dictionary.
        public void NewGame(string userId)
        {
            if (Games.TryGetValue(userId, out Game game))
            {
                game.NewGame();
            }
            else
            {
                Games.Add(userId, new Game(userId));
            }
           
        }

        //Restarts the game
        public void RestartGame(string playerID)
        {
            Games[playerID].NewGame();
        }

        //Places AI ships randomly
        public void PlaceAIShipsRandomly(string userId)
        {
            Games[userId].PlaceAIShipsRandomly();
        }

        //Places player ships randomly.
        public string PlacePlayerShipsRandomly(string userId)
        {
            return Games[userId].PlacePlayerShipsRandomly();
        }

        /// <summary>
        /// Returns a string message in the form of "[result of shot] [column] [row] [accuracy(if you won on this shot)]"
        /// Result of shot is either "HIT", "MISS", "WIN", or the name of the ship you just sunk
        /// This method takes in the input from the front end (in the form of 1-10) converts it to 0-9, and returns it in the form of 1-10
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
        /// Returns a string message in the form of "[result of shot][space][column][space][row]"
        /// Result of shot is either "HIT", "MISS", "LOSE", or the name of the ship the AI just sunk
        /// </summary>
        /// <returns></returns>
        public string AIShootDumb(string userId)
        {
            string[] response = Games[userId].AIShootDumb().Split(new char[] { ' ' });
            string resultOfShot = response[0];
            int xCoordinate = Convert.ToInt32(resultOfShot[1] + 1);
            int yCoordinate = Convert.ToInt32(resultOfShot[2] + 1);
            return resultOfShot + " " + xCoordinate + " " + yCoordinate;
        }

        /// <summary>
        /// Returns a string message in the form of "[result of shot][space][column][space][row]"
        /// Result of shot is either "HIT", "MISS", "LOSE", or the name of the ship the AI just sunk
        /// </summary>
        /// <returns></returns>
        
        
         public string AIShootSmart(string userId)
        {
            string[] response = Games[userId].AIShootSmart().Split(new char[] { ' ' });
            string resultOfShot = response[0];
            int xCoordinate = Convert.ToInt32(resultOfShot[1] + 1);
            int yCoordinate = Convert.ToInt32(resultOfShot[2] + 1);
            return resultOfShot + " " + xCoordinate + " " + yCoordinate;

        }

        //Returns the AIShootSmart method
        public string AIShoot(string playerID)
        {
            string resultOfShot = Games[playerID].AIShootSmart();
            return resultOfShot;
        }

        /// <summary>
        /// Return the Player accuracy score after winning the game
        /// </summary>
        /// <param name="playerUsername"></param>
        /// <returns></returns>
        public double GetAccuracyScore(string playerUsername)
        {
            return Games[playerUsername].Player.GetAccuracyScore();
        }

        //Returns the rocket barrage method.

        public string RocketBarrage(string playerUsername)
        {
            return Games[playerUsername].RocketBarrage();
        }
    }
}

