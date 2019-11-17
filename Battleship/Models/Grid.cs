using System;
using System.Collections;
using System.Collections.Generic;

namespace Battleship.Models
{
    public class Grid
    {
        public Cell[,] Cells { get; set; }

        public Dictionary<string, Ship> Ships;

        public Grid()
        {
            this.Cells = new Cell[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }

            Ships = new Dictionary<string, Ship>
            {
                { Ship.CARRIER, new Ship(Ship.CARRIER) },
                { Ship.BATTLESHIP, new Ship(Ship.BATTLESHIP) },
                { Ship.CRUISER, new Ship(Ship.CRUISER) },
                { Ship.SUBMARINE, new Ship(Ship.SUBMARINE) },
                { Ship.DESTROYER, new Ship(Ship.DESTROYER) },
            };
        }

        public bool IsValidShipPlacement(int shipLength, int column, int row, bool isVertical)
        {
            if (column > 9 || column < 0 || row > 9 || row < 0)
            {
                return false;
            }

            return AllBlocksOfShipAreInBounds(shipLength, column, row, isVertical) 
                && SpotsAndNeighborsAreClear(shipLength, column, row, isVertical);
        }

        public bool AllBlocksOfShipAreInBounds(int shipLength, int column, int row, bool isVertical)
        {
            if (isVertical)
            {
                return row + shipLength - 1 <= 9;
            }

            else
            {
                return column + shipLength - 1 <= 9;
            }
        }

        public bool SpotsAndNeighborsAreClear(int shipLength, int column, int row, bool isVertical)
        {
            if (isVertical)
            {
                for (int r = row; r < row + shipLength; r++)
                {
                    if (!Cells[column, r].PartialShip.Equals(Cell.NONE))
                    {
                        return false;
                    }

                    if (NeighborHasShip(column + 1, r) || NeighborHasShip(column - 1, r) || NeighborHasShip(column, r + 1) || NeighborHasShip(column, r - 1))
                    {
                        return false;
                    }
                }
            }

            else 
            {
                for (int c = column; c < column + shipLength; c++)
                {
                    if (!Cells[c, row].PartialShip.Equals(Cell.NONE))
                    {
                        return false;
                    }

                    if (NeighborHasShip(c+1, row) || NeighborHasShip(c-1, row) || NeighborHasShip(c, row+1) || NeighborHasShip(c, row-1))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool NeighborHasShip(int column, int row)
        {
            if (column > 9 || column < 0 || row > 9 || row < 0)
            {
                return false;
            }

            return !Cells[column, row].PartialShip.Equals(Cell.NONE);
        }

        /// <summary>
        /// Returns true if the ship placement is valid and successful.
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="isVertical"></param>
        /// <returns></returns>
        public bool PlaceShipSuccessful(string shipType, int column, int row, bool isVertical)
        {
            if (!Ship.ShipLengths.TryGetValue(shipType, out int shipLength))
            {
                throw new ArgumentException(shipType + " is not a valid ship type.");
            }

            if (!IsValidShipPlacement(shipLength, column, row, isVertical))
            {
                return false;
            }

            if (isVertical)
            {
                // Place the ship
                for (int i = row; i < row + shipLength; i++)
                {
                    Cells[i, column].PartialShip = shipType;
                }
            }

            else
            {
                for (int i = column; i < column + shipLength; i++)
                {
                    Cells[row, i].PartialShip = shipType;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns "MISS" if it's a miss.
        /// Returns "HIT" if it's a hit that didn't sink a ship.
        /// Returns the name of the ship if the hit resulted in a sunk ship.
        /// Returns "DUPLICATE" if the cell has already been shot at.
        /// Throws an Argument Exception if the shot is at an invalid location.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string ShootCell(int column, int row)
        {
            if (column > 9 || column < 0 || row > 9 || row < 0)
            {
                throw new ArgumentException("You can't shoot at the coordinates (" + column + "," + row + "). Must be between (0,0) and (9,9)");
            }

            Cell cell = Cells[column, row];

            if (cell.HitMissOrNone.Equals(Cell.NONE))
            {
                var shipAtLocation = cell.PartialShip;
                if (shipAtLocation.Equals(Cell.NONE))
                {
                    cell.HitMissOrNone = Cell.MISS;
                    return Cell.MISS;
                }

                cell.HitMissOrNone = Cell.HIT;
                Ships[shipAtLocation].Hit();
                if (Ships[shipAtLocation].IsSunk())
                {
                    return shipAtLocation;
                }

                else return Cell.HIT;
            }

            else return "DUPLICATE";
        }

        public void WipeGrid()
        {
            this.Cells = new Cell[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }

            Ships = new Dictionary<string, Ship>
            {
                { Ship.CARRIER, new Ship(Ship.CARRIER) },
                { Ship.BATTLESHIP, new Ship(Ship.BATTLESHIP) },
                { Ship.CRUISER, new Ship(Ship.CRUISER) },
                { Ship.SUBMARINE, new Ship(Ship.SUBMARINE) },
                { Ship.DESTROYER, new Ship(Ship.DESTROYER) },
            };
        }

        public void PlaceShipsRandomly()
        {
            this.WipeGrid();

            Random random = new Random();

            foreach (string type in Ship.ShipTypes)
            {
                bool placementWasSuccessful = false;
                while (!placementWasSuccessful)
                {
                    placementWasSuccessful = this.PlaceShipSuccessful(type, random.Next(0, 9), random.Next(0, 9), random.Next(0, 1) == 0);
                }
            }
        }
    }
}