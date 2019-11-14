using System.Collections;
using System.Collections.Generic;

namespace Battleship.Models
{
    public class Grid
    {
        private Cell[,] Cells { get; set; }
        private string GameId { get; set; }
        private List<Ship> Ships { get; set; }

        public Grid()
        {
            this.Cells = new Cell[10, 10];
            this.GameId = "temp"; //This will be used and improved later.

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }

            Ships = new List<Ship>
            {
                new Ship(Ship.CARRIER),
                new Ship(Ship.BATTLESHIP),
                new Ship(Ship.CRUISER),
                new Ship(Ship.SUBMARINE),
                new Ship(Ship.DESTROYER)
            };
        }
    }
}