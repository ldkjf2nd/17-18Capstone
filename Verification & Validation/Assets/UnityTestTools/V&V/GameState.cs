using System;

namespace AssemblyCSharp
{
	public class GameState
	{
		public bool isANewGame = true;

		public void startGame()
		{
			if (isANewGame)
				//Start a new game
				//Set isANewGame to false for when user returns to this game state.
				isANewGame = false;
			else
				//Load last save's game state.
				loadGame();
		}

		public void loadNextLevel()
		{
			//will be implemented later
		}

		public void reloadCurrentLevel(object currentGameState)
		{
			//will be implemented later
		}
		public void loadGame()
		{
			//will be implemented later
		}

		public void saveGame()
		{
			//will be implemented later
		}


		public void pauseGame() 
		{
			//will be implemented later
		}

		public void unPauseGame() 
		{
			//will be implemented later
		}
	}
}

