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

		public void interactWithItem(object item)
		{
			//Will be implemented later
			//if item == health potion
			//	healplayer()
			//etc.
		}

		public void viewPlayerStatus()
		{
			//Will be implemented later

		}

		public void viewGearStatus()
		{
			//Will be implemented later

		}
			
		public void viewItems()
		{
			//Will be implemented later

		}

		public void performMeleeAttack()
		{
			//Will be implemented later

		}

		public void performRangedAttack()
		{
			//Will be implemented later

		}

		public void performComboAttack()
		{
			//Will be implemented later

		}
		public void showGameOverScreen()
		{
			//will be implemented later
		}
	}
}

