using System;

namespace Capstone
{
	public class HealthComponent
	{
		Player player = new Player ();

		public float playerHealth = player.playerHealth;
		public float enemyHealth;
		public bool isPlayerDead = false;

		public void PlayerTakesDamage(float damageAmount) 
		{
			playerHealth -= damageAmount;
			hasPlayerDied ();
		}

		public void EnemyTakesDamage(float damageAmount) 
		{
			enemyHealth -= damageAmount;
		}

		public void hasPlayerDied() 
		{
			if (playerHealth == 0f)
				isPlayerDead = true;
				showGameOverScreen ();
		}

		public void showGameOverScreen()
		{
			//will be implemented later
		}
	}
}

