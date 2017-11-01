using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class BaseEnemyTests
	{
		BaseEnemy enemy = new BaseEnemy();
		RobotEnemy robotEnemy = new RobotEnemy();


		[Test]
		public void EnemyTakesDamage_PositiveAmount_HealthUpdated() 
		{
			enemy.enemyHealth = 100f;

			enemy.EnemyTakesDamage (10f);

			Assert.AreEqual (95f, enemy.enemyHealth);
		}

		[Test]
		public void EnemyHasBaseArmorRating_TakesPositiveDamage_ShouldOnlyTakeHalfTheAmountOfDamageDealt() 
		{
			float healthLoss = enemy.calculateHealthLossAfterArmor (10f);

			Assert.AreEqual (5f, healthLoss);
		}

		[Test]
		public void PlayerAppearsOnScreen_EnemyShouldSuccessfullyFindThemOnScreen() 
		{
			enemy.findPlayerOnScreen ();

			Assert.Fail ();
		}

		[Test]
		public void PlayerAppearsOnScreen_EnemyShouldStartUsingBasicAttack() 
		{
			enemy.useBasicAttack ();

			Assert.Fail ();
		}

		[Test]
		public void EnemyFallsBelow30PercentHP_StartUsingSpecialAttack() 
		{
			enemy.useSpecialAttack ();

			Assert.Fail ();
		}

		[Test]
		public void PlayerReachesCertainArea_ShouldSpawnEnemy() 
		{
			enemy.showSpawnAnimation ();

			Assert.Fail ();
		}

		[Test]
		public void EnemyReaches0HP_ShouldShowDeathAnimation() 
		{	
			if (enemy.enemyHealth <= 0)
				enemy.showDeathAnimation ();

			Assert.Fail ();
		}


		[Test]
		public void EnemyReaches0HP_ShouldDropScrap_IfThereIsAChance() 
		{	
			if (enemy.enemyHealth <= 0)
				enemy.dropScrap ();

			Assert.Fail ();
		}

		[Test]
		public void RobotEnemyShouldFireProjectileEverySecond() 
		{
			Assert.Fail ();
		}
			
	}
}

