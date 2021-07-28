using System;
using System.Collections;
using System.Collections.Generic;
using BattleshipStateTracker.Models;
using System.Linq;

namespace BattleshipStateTracker.Core
{
    internal class BattleshipGameService : IBattleshipGameService
    {
        #region Variables

        readonly IList<IList<BoardCell>> _board;
        readonly IList<Ship> _ships;

        #endregion

        public BattleshipGameService()
        {
            // Initiliaze the board and ship instances
            _board = new List<IList<BoardCell>>();
            _ships = new List<Ship>();
        }

        #region Public methods

        /// <summary>
        /// Returns the status of the game.
        /// </summary>
        public bool IsGameOver
        {
            get => !_ships.Any(s => s.UnAttackedCellCount > 0);
        }

        /// <summary>
        /// Initialize the Battle board with the given board size
        /// </summary>
        /// <param name="boardSize"></param>
        public void InitializeBattleBoard(int boardSize)
        {
            for (int i = 0; i < boardSize; i++)
            {
                _board.Add(new List<BoardCell>());
                for (int j = 0; j < boardSize; j++)
                {
                    _board[i].Add(new BoardCell(i, j));
                }
            }
        }

        /// <summary>
        /// Initialize the ship and place it in the board
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public bool InitializeShip(Ship ship)
        {
            if(IsValidPosition(ship.PositionX, ship.PositionY))
            {
                var isPlaced = PlaceShip(ship);
                if (isPlaced)
                    _ships.Add(ship);

                return isPlaced;
            }
            return false;
        }

        /// <summary>
        /// Attacks the cell of given coordinates and returns the hit or miss.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public (BoardCellFill BoardCellFill, bool IsGameOver) Attack(int x, int y)
        {
            if (!IsGameOver)
            {
                if (IsValidPosition(x, y))
                {
                    switch (_board[x][y].CellFillType)
                    {
                        case BoardCellFill.Water:
                            _board[x][y].CellFillType = BoardCellFill.Attacked;
                            return (BoardCellFill.Water, false);

                        case BoardCellFill.UnAttacked:
                            var shipToAttack = _ships.FirstOrDefault(s => s.ShipId == _board[x][y].ShilId);
                            if (shipToAttack == null)
                                throw new BattleshipGeneralException("Something went wrong!.");

                            shipToAttack.UnAttackedCellCount--;

                            if (shipToAttack.UnAttackedCellCount > 0)
                            {
                                _board[x][y].CellFillType = BoardCellFill.Attacked;
                                return (BoardCellFill.Attacked, false);
                            }
                            else
                            {
                                _board[x][y].CellFillType = BoardCellFill.Sunk;
                                return (BoardCellFill.Sunk, IsGameOver);
                            }

                        case BoardCellFill.Attacked:
                            throw new AttackedBattleshipCellException("This cell was attacked already!", _board[x][y]);
                        case BoardCellFill.Sunk:
                            throw new SunkenBattleshipException("This ship is already sunken!");
                        default:
                            throw new BattleshipGeneralException("Something went wrong!");
                    }
                }
                else
                    throw new InvalidBattleshipInputException("Attack coordinates are invalid");
            }
            else
                throw new BattleShipGameoverException("The game is over!");

        }

        /// <summary>
        /// Clears the Board
        /// </summary>
        public void ClearBoard()
        {
            _board.Clear();
            _ships.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks and validates the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsValidPosition(int x, int y)
        {
            return x < _board.Count && y < _board[x].Count;
        }

        /// <summary>
        /// Place the ship in the battleship board
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool PlaceShip(Ship ship)
        {
            if (ship.Orientation == ShipOrientation.Horizontal)
            {
                if((ship.PositionX + ship.Length) < _board.Count)
                {
                    for (int currentCell = ship.PositionX; currentCell <= (ship.PositionX + ship.Length); currentCell++)
                    {
                        if (_board[currentCell][ship.PositionY].CellFillType != BoardCellFill.Water)
                        {
                            return false;
                        }
                    }

                    for (int currentCell = ship.PositionX; currentCell < (ship.PositionX + ship.Length); currentCell++)
                    {
                        var cell = _board[currentCell][ship.PositionY];
                        cell.CellFillType = BoardCellFill.UnAttacked;
                        cell.ShilId = ship.ShipId;
                    }

                    return true;
                }

                return false;
            }
            else
            {
                if ((ship.PositionY + ship.Length) < _board[ship.PositionX].Count)
                {
                    for (int currentCell = ship.PositionY; currentCell <= (ship.PositionY + ship.Length); currentCell++)
                    {
                        if (_board[ship.PositionX][currentCell].CellFillType != BoardCellFill.Water)
                        {
                            return false;
                        }
                    }

                    for (int currentCell = ship.PositionY; currentCell <= (ship.PositionY + ship.Length); currentCell++)
                    {
                        _board[ship.PositionX][currentCell].CellFillType = BoardCellFill.UnAttacked;
                        var cell = _board[currentCell][ship.PositionY];
                        cell.CellFillType = BoardCellFill.UnAttacked;
                        cell.ShilId = ship.ShipId;
                    }
                    return true;
                }

                return false;
            }
        }

        #endregion

        
    }
}
