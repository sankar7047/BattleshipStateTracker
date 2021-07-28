using System;
using BattleshipStateTracker.Models;

namespace BattleshipStateTracker.Core
{
    public class AttackedBattleshipCellException : Exception
    {
        public AttackedBattleshipCellException(string message, BoardCell boardCell) : base(message)
        {
            BoardCell = boardCell;
        }

        public BoardCell BoardCell { get; set; }
    }
}
