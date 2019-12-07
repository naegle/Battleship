/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    The unit tests for the project.
*/
using Battleship.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GamesService g = new GamesService();
            string userId = "some guy";
            g.NewGame(userId);

            g.PlaceAIShipsRandomly(userId);
            string AIShips = "";
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    AIShips += g.Games[userId].AIGrid.Cells[i,j].PartialShip.Substring(0,3) + "\t";
                }

                AIShips += "\n";
            }

            g.PlacePlayerShipsRandomly(userId);
            string playerShips = "";
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    playerShips += g.Games[userId].PlayerGrid.Cells[i, j].PartialShip.Substring(0, 3) + "\t";
                }

                playerShips += "\n";
            }

            string runningResultPlayer = "";
            string runningResultAI = "";

            bool done = false;
            for (int i = 0; i <= 9; i++)
            {
                if (done)
                {
                    break;
                }

                for (int j = 0; j <= 9; j++)
                {
                    runningResultPlayer += g.PlayerShoot(userId, i, j).Substring(0,3) + "\t";
                    if (runningResultPlayer.Contains("WIN"))
                    {
                        done = true;
                        break;
                    }

                    runningResultAI += g.AIShootDumb(userId).Substring(0,3) + "\t";

                    if (runningResultAI.Contains("Los"))
                    {
                        done = true;
                        break;
                    }
                }

                runningResultPlayer += "\n";
                runningResultAI += "\n";
            }

            if (!runningResultPlayer.Contains("WIN") && !runningResultAI.Contains("WIN"))
            {
                runningResultPlayer += "\nNO WIN";
                runningResultAI += "\nNO Los";
            }
        }
    }
}
