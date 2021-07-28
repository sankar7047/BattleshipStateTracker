using System;
namespace BattleshipStateTracker.Core
{
    public class BattleshipFactoryService
    {
        public static IBattleshipGameService GetBattleshipService()
        {
            return new BattleshipGameService();
        }
    }
}
