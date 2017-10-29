using System;

namespace Capstone
{
	public class Enemy
	{
		public float enemyHealth;
		public bool doesDropScrap;
		public float chanceToDropScrap;

		public void EnemyTakesDamage(float damageAmount) 
		{
			enemyHealth -= damageAmount;
		}

		public void dropScrap()
		{
				
		}
	}
}

