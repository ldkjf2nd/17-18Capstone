using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class HealthComponentTests
	{
		HealthComponent health = new HealthComponent ();

		[Test]
		public void PlayerTakesDamage_PositiveAmount_HealthUpdated() 
		{
			health.playerHealth = 100f;

			health.PlayerTakesDamage (10f);
			Assert.AreEqual (90f, health.playerHealth);
		}

		[Test]
		public void EnemyTakesDamage_PositiveAmount_HealthUpdated() 
		{
			health.enemyHealth = 100f;

			health.EnemyTakesDamage (50f);
			Assert.AreEqual (50f, health.enemyHealth);
		}

		[Test]
		public void PlayerTakesFatalDamage_isPlayerDeadBoolSetToTrue() 
		{
			health.playerHealth = 100f;

			health.PlayerTakesDamage (100f);
			Assert.AreEqual (true, health.isPlayerDead);
		}
	}
}

