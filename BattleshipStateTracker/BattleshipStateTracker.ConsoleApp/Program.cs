using System;
using BattleshipStateTracker.Core;
using BattleshipStateTracker.Models;

namespace BattleshipStateTracker.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- Welcome to Battleship Game -----");
            var rand = new Random();
            var battershipService = BattleshipFactoryService.GetBattleshipService();

            battershipService.InitializeBattleBoard(10); // Initializing the board as 10*10

            int shipsToAttack = 4; // Initializing the ships thats going to be add on the board
            int addedShipsCount = 0; // added ships count

            while(addedShipsCount <= shipsToAttack)
            {
                // Initializing the ships
                if (battershipService.InitializeShip(new Ship(rand.Next(0, 9), rand.Next(0, 9), rand.Next(2, 4), rand.Next(0, 20) % 2 == 0 ? ShipOrientation.Horizontal : ShipOrientation.Vertical)))
                    addedShipsCount++;
            }

            Console.WriteLine("\nEnter the coordinates to attack the ship. \nPlease enter the coordinates with a space. Eg. 4 5 to the (4,5) cell.\n");

            bool isGameOn = true;
            while(isGameOn) // Allows to retry and continue the game until the game over or user exits the game.
            {
                var command = Console.ReadLine();

                switch (command.ToUpper())
                {
                    case "EXIT":
                        isGameOn = false;
                        Console.WriteLine("Exiting the game!");
                        break;
                    default:
                        var indices = command.Split(" ");

                        if(indices.Length != 2)
                        {
                            Console.WriteLine("Invalid input! Please try again.");
                            continue;
                        }

                        if (int.TryParse(indices[0], out int x) && int.TryParse(indices[1], out int y))
                        {
                            try
                            {
                                var attackResult = battershipService.Attack(x, y);

                                if(attackResult.IsGameOver)
                                {
                                    Console.WriteLine("Congratulations! You have sunken all the ships.");
                                }
                                else
                                {
                                    switch (attackResult.BoardCellFill)
                                    {
                                        case BoardCellFill.Attacked:
                                            Console.WriteLine("That's a hit! Keep trying to sunk the ship.");
                                            break;
                                        case BoardCellFill.Water:
                                            Console.WriteLine("You missed it! Keep trying.");
                                            break;
                                        case BoardCellFill.Sunk:
                                            Console.WriteLine("That's a hit! The ship is sunk.");
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            // Caught different exceptions to do UI logics based on the exception type, here just printing the exception message
                            catch (AttackedBattleshipCellException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (SunkenBattleshipException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (BattleshipGeneralException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch(InvalidBattleshipInputException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (BattleShipGameoverException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid input! Please try again.");
                            continue;
                        }
                        break;

                }

                
            }
            
        }
    }
}
