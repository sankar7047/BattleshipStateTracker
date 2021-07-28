using System;
namespace BattleshipStateTracker.Models
{
    public class Ship
    {
        public Ship(int positionX, int positionY, int length, ShipOrientation shipOrientation)
        {
            PositionX = positionX;
            PositionY = positionY;
            Length = UnAttackedCellCount = length;
            Orientation = shipOrientation;
        }

        public Guid ShipId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Length { get; set; }
        public int UnAttackedCellCount { get; set; }
        public ShipOrientation Orientation { get; set; }
    }
}
