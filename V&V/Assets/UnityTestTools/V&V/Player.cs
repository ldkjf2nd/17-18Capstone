using System;

namespace Capstone
{
	public class Player
	{
		public float playerHealth;
		public bool isPlayerDead = false;
		public object equippedWeapon;

		public void PlayerTakesDamage(float damageAmount) 
		{
			if (damageAmount > 0)
				playerHealth -= damageAmount;

			if (playerHealth < 0)
				playerHealth = 0;
			
			hasPlayerDied ();
		}

		public void hasPlayerDied() 
		{
			if (playerHealth == 0f)
				isPlayerDead = true;
			showGameOverScreen ();
		}

		public void pickUpWeapon(object wepaon)
		{
			equippedWeapon = wepaon;
			
		}

		public void viewPlayerStatus()
		{
			//Will be implemented later

		}

		public void viewGearStatus()
		{
			//Will be implemented later

		}
			
		public void showGameOverScreen()
		{
			//will be implemented later
		}
	}
}

