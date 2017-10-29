using System;

namespace Capstone
{
	public class Enemy
	{
		public float enemyHealth;

		public void EnemyTakesDamage(float damageAmount) 
		{
			enemyHealth -= damageAmount;
		}
	}
}

