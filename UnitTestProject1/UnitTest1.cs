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

                    runningResultAI += g.AIShoot(userId).Substring(0,3) + "\t";

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
