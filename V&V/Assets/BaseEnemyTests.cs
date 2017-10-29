using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class BaseEnemyTests
	{
		BaseEnemy enemy = new BaseEnemy();


		[Test]
		public void EnemyTakesDamage_PositiveAmount_HealthUpdated() 
		{
			enemy.enemyHealth = 100f;

			enemy.EnemyTakesDamage (10f);

			Assert.AreEqual (90f, enemy.enemyHealth);
		}
	}
}

