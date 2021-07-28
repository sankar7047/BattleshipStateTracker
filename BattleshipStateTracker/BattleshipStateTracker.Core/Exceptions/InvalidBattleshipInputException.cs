using System;
namespace BattleshipStateTracker.Core
{
    public class InvalidBattleshipInputException : InvalidOperationException
    {
        public InvalidBattleshipInputException(string message) : base(message)
        {
        }
    }
}
