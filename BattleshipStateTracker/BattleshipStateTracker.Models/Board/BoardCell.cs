using System;
namespace BattleshipStateTracker.Models
{
    public class BoardCell
    {
        public BoardCell(int rowX, int columnY, BoardCellFill cellFill = BoardCellFill.Water)
        {
            RowX = rowX;
            ColumnY = columnY;
            CellFillType = cellFill;
        }

        public Guid ShilId { get; set; }
        public int RowX { get; set; }
        public int ColumnY { get; set; }
        public BoardCellFill CellFillType { get; set; }
    }
}
