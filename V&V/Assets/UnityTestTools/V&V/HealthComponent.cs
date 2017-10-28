using System;

namespace Capstone
{
	public class HealthComponent
	{
		public float playerHealth;
		public float enemyHealth;

		public void TakeDamage(float damageAmount) 
		{
			playerHealth -= damageAmount;
		}
	}
}

