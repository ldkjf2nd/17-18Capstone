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

		public void interactWithGundamGear()
		{
			//Will be implemented later

		}

		public void showDeathAnimation()
		{
			//Will be implemented later
		}

		public void openShopMenu()
		{
			//Will be implemented later
		}
		public void showGameOverScreen()
		{
			//Will be implemented later
		}
	}
}

