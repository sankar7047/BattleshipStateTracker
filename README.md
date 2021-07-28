# Battleship State Tracker
- This is a Console application implemented using C#, .Net 5 and .Net Standard

# Structure
- Program.cs to kick start the application
- BattleshipFactoryService is used to get the instance of IBattleshipGameService.
- Internally the BattleshipFactoryService creates and returns an instance of BattleshipGameService which of type IBattleshipGameService
- Models - BoardCell and Ship are used to implement the game logics.
- Exception folder has custom exception classes used in the logic.

# Requirements
- Visual Studio
- .Net 5
- C#

# Implementation 
- InitializeBattleBoard method will initialize the board with given board size
- InitializeShip method will initialize and place the ship in the board
- If it used to place another ship that overlaps the other ship, then the operation will be failed
- Attack method will gets the coordinates and try to attack the cell.
- If the cell is not filled with a ship, then that is a miss.
- If the cell is filled with a ship, then that is a hit.
- If all the cells of the ship is attacked, then that is a sunk.

# Output
- ----- Welcome to Battleship Game -----

- Enter the coordinates to attack the ship. 
- Please enter the coordinates with a space. Eg. 4 5 to the (4,5) cell.

- 4 6
- You missed it! Keep trying.
- 3 5
- That's a hit! Keep trying to sunk the ship.
- 4,8
- Invalid input! Please try again.
- 4 6
- This cell was attacked already!
- 3 6
- You missed it! Keep trying.
- 4 5
- That's a hit! Keep trying to sunk the ship.
- 5 5
- You missed it! Keep trying.
- Exit
- Exiting the game!
