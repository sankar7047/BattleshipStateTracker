using System;
namespace BattleshipStateTracker.Core
{
    public class BattleShipGameoverException : Exception
    {
        public BattleShipGameoverException(string message) : base(message)
        {
        }
    }
}
