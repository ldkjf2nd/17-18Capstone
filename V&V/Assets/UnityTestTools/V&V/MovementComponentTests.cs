using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class MovementComponentTests
	{
		MovementComponent playerMovement = new MovementComponent ();

		[Test]
		public void PlayerMovesAlongXAxis_PositiveDirection_PlayerLocationUpdated() 
		{
			playerMovement.playerXPos = 0f;

			playerMovement.movePlayerHorizontally(5f);

			Assert.AreEqual (5f, playerMovement.playerXPos);

		}

		[Test]
		public void PlayerMovesAlongYAxis_PostiveDirection_PlayerLocationUpdated() 
		{
			playerMovement.playerYPos = 0f;

			playerMovement.playerJump();

			Assert.AreEqual (2.5f, playerMovement.playerYPos);
		}
	}
}