using System;
using BattleshipStateTracker.Models;

namespace BattleshipStateTracker.Core
{
    public interface IBattleshipGameService
    {
        /// <summary>
        /// Returns the status of the game.
        /// </summary>
        bool IsGameOver { get; }

        /// <summary>
        /// Initialize the Battle board with the given board size
        /// </summary>
        /// <param name="boardSize"></param>
        void InitializeBattleBoard(int boardSize);

        /// <summary>
        /// Initialize the ship and place it in the board
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>The success of the ship placement</returns>
        bool InitializeShip(Ship ship);

        /// <summary>
        /// Attacks the cell of given coordinates and returns the hit or miss.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The board cell and the status of the game.</returns>
        (BoardCellFill BoardCellFill, bool IsGameOver) Attack(int x, int y);

        /// <summary>
        /// Clears the Board
        /// </summary>
        void ClearBoard();
    }
}
