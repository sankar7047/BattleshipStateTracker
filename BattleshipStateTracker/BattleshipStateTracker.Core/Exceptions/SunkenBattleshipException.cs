using System;
namespace BattleshipStateTracker.Core
{
    public class SunkenBattleshipException : Exception
    {
        public SunkenBattleshipException(string message) : base(message)
        {
        }
    }
}
