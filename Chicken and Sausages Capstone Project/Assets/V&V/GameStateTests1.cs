using System;
using NUnit.Framework;

namespace AssemblyCSharp
{
	public class GameStateTests
	{
		GameState game = new GameState();

		[Test]
		public void UserPressesButtonToPauseGame_ShowPauseMenu()
		{
			game.pauseGame ();
			Assert.Fail ();
		}

		[Test]
		public void UserPressesButtonToUnPauseGame_ReturnToGame()
		{
			game.unPauseGame ();
			Assert.Fail ();
		}

		[Test]
		public void UserStartsABrandNewGame_StartANewPlaythrough()
		{
			game.startGame ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerReachesACheckPoint_SaveCurrentGameState()
		{
			game.saveGame ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerCompletesALevel_ProceedtoNextLevel()
		{
			game.loadNextLevel ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerDies_ReloadCurrentLevel()
		{
			Object curentLevel = new Object ();
			game.reloadCurrentLevel(curentLevel);
			Assert.Fail ();
		}
	}
}

