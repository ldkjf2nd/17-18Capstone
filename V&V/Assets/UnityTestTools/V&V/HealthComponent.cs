using System;

namespace Capstone
{
	public class HealthComponent
	{
		public float playerHealth;
		public float enemyHealth;

		public void PlayerTakesDamage(float damageAmount) 
		{
			playerHealth -= damageAmount;
		}

		public void EnemyTakesDamage(float damageAmount) 
		{
			enemyHealth -= damageAmount;
		}
	}
}

