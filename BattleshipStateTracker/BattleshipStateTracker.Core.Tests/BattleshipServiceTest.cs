using NUnit.Framework;

namespace BattleshipStateTracker.Core.Tests
{
    public class BattleshipServiceTest
    {
        IBattleshipGameService battleship;

        [SetUp]
        public void Setup()
        {
            battleship = BattleshipFactoryService.GetBattleshipService();
            battleship.InitializeBattleBoard(10);
        }

        [Test]
        public void BattleshipFactoryService_Instance_Check_Test()
        {
            Assert.IsTrue(battleship != null, "BattleshipGameService instance is null");
        }

        [Test]
        public void BattleshipGame_InitializeShip_Success_Test()
        {
            var isPlaced = battleship.InitializeShip(new Models.Ship(2, 4, 3, Models.ShipOrientation.Horizontal));

            Assert.IsTrue(isPlaced, "Ship initializing is failed.");
        }

        [Test]
        public void BattleshipGame_InitializeShip_Failure_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 4, 3, Models.ShipOrientation.Horizontal));
            var isPlaced = battleship.InitializeShip(new Models.Ship(3, 4, 3, Models.ShipOrientation.Horizontal));

            Assert.IsFalse(isPlaced, "Initializing a ship on the exisitng position check is failed.");
        }

        [Test]
        public void BattleshipGame_InitializeShip_Success_Vertical_Placement_Test()
        {
            var isPlaced = battleship.InitializeShip(new Models.Ship(2, 4, 3, Models.ShipOrientation.Vertical));

            Assert.IsTrue(isPlaced, "Ship initializing is failed.");
        }

        [Test]
        public void BattleshipGame_InitializeShip_Failure_Vertical_Placement_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 4, 3, Models.ShipOrientation.Vertical));
            var isPlaced = battleship.InitializeShip(new Models.Ship(2, 5, 3, Models.ShipOrientation.Vertical));

            Assert.IsFalse(isPlaced, "Initializing a ship on the exisitng position check is failed.");
        }

        [Test]
        public void BattleshipGame_Attack_Miss_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 5, 3, Models.ShipOrientation.Vertical));

            var attackResult = battleship.Attack(6, 9);

            Assert.IsTrue(attackResult.BoardCellFill == Models.BoardCellFill.Water, "Attack of non ship cell is failed");
        }

        [Test]
        public void BattleshipGame_Attack_Attack_Hit_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 5, 3, Models.ShipOrientation.Vertical));

            var attackResult = battleship.Attack(2, 5);

            Assert.IsTrue(attackResult.BoardCellFill == Models.BoardCellFill.Attacked, "Attack of ship in a cell is failed");
        }

        [Test]
        public void BattleshipGame_Attack_Sunk_Hit_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 5, 3, Models.ShipOrientation.Vertical));

            battleship.Attack(2, 5);
            battleship.Attack(2, 6);
            var attackResult = battleship.Attack(2, 7);

            Assert.IsTrue(attackResult.BoardCellFill == Models.BoardCellFill.Sunk, "Sunk of ship is failed");
        }

        [Test]
        public void BattleshipGame_Attack_Sunk_Hit_GameOver_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 5, 3, Models.ShipOrientation.Vertical));

            battleship.Attack(2, 5);
            battleship.Attack(2, 6);
            var attackResult = battleship.Attack(2, 7);

            Assert.IsTrue(attackResult.IsGameOver, "Game is not over yet");
        }

        [Test]
        public void BattleshipGame_IsGameOver_Test()
        {
            battleship.InitializeShip(new Models.Ship(2, 5, 3, Models.ShipOrientation.Vertical));

            battleship.Attack(2, 5);
            battleship.Attack(2, 6);
            battleship.Attack(2, 7);

            Assert.IsTrue(battleship.IsGameOver, "Game is not over yet");
        }
    }
}
