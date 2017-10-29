using System;

namespace Capstone
{
	public class Player
	{
		public float playerHealth;
		public bool isPlayerDead = false;

		public void PlayerTakesDamage(float damageAmount) 
		{
			if (damageAmount > 0)
				playerHealth -= damageAmount;
			
			hasPlayerDied ();
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

