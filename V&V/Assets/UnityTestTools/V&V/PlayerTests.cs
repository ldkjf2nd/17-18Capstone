using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class PlayerTests
	{
		Player player = new Player();
		[Test]
		public void PlayerTakesDamage_PositiveAmount_HealthUpdated() 
		{
			player.playerHealth = 100f;

			player.PlayerTakesDamage (10f);
			Assert.AreEqual (90f, player.playerHealth);
		}

		[Test]
		public void PlayerTakesFatalDamage_isPlayerDeadBoolSetToTrue() 
		{
			player.playerHealth = 100f;

			player.PlayerTakesDamage (100f);
			Assert.AreEqual (true, player.isPlayerDead);
		}
	}
}

