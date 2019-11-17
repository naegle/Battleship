using Battleship.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            string runningResult = "";
            
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    runningResult += g.PlayerShoot(gameId, i, j).Substring(0,3) + "\t";
                }

                runningResult += "\n";
            }

            if (!runningResult.Contains("WIN"))
            {
                runningResult += "\nNO WIN";
                //Assert.Fail();
            }
        }
    }
}
