using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class EnemyTests
	{
		Enemy enemy = new Enemy ();

		[Test]
		public void EnemyTakesDamage_PositiveAmount_HealthUpdated() 
		{
			enemy.enemyHealth = 100f;

			enemy.EnemyTakesDamage (50f);
			Assert.AreEqual (50f, enemy.enemyHealth);
		}
	}
}

