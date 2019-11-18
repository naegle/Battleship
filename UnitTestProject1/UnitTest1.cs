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
            int gameId = g.NewGame();

            g.PlaceAIShips(gameId);
            string AIShips = "";
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    AIShips += g.Games[gameId].AIGrid.Cells[i,j].PartialShip.Substring(0,3) + "\t";
                }

                AIShips += "\n";
            }

            g.PlacePlayerShipsRandomly(gameId);
            string playerShips = "";
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    playerShips += g.Games[gameId].PlayerGrid.Cells[i, j].PartialShip.Substring(0, 3) + "\t";
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
                    runningResultPlayer += g.PlayerShoot(gameId, i, j).Substring(0,3) + "\t";
                    if (runningResultPlayer.Contains("WIN"))
                    {
                        done = true;
                        break;
                    }

                    runningResultAI += g.AIShoot(gameId).Substring(0,3) + "\t";

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
                //Assert.Fail();
            }

            int a = 0;
        }
    }
}
