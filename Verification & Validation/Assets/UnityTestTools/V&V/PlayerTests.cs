using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class PlayerTests
	{
		Player player = new Player();


		[Test]
		public void PlayerInteractsWithShop_NearTheShop() 
		{
			bool isPlayerNearTheShop = true;
			if (isPlayerNearTheShop)
				player.openShopMenu ();

			Assert.Fail ();
		}

		[Test]
		public void PlayerTakesDamage_PositiveAmount_HealthUpdated() 
		{
			player.playerHealth = 100f;

			player.PlayerTakesDamage (10f);
			Assert.AreEqual (90f, player.playerHealth);
		}

		[Test]
		public void PlayerTakesFatalDamage_isPlayerDeadBoolSetToTrue() 
		{
			player.playerHealth = 100f;

			player.PlayerTakesDamage (100f);
			Assert.AreEqual (true, player.isPlayerDead);
		}

		[Test]
		public void PlayerTakesDamage_NegativeAmount_HealthShouldNotUpdate() 
		{
			player.playerHealth = 100f;

			player.PlayerTakesDamage (-10f);
			Assert.AreEqual (100f, player.playerHealth);
		}

		[Test]
		public void PlayerTakesDamage_PostitiveAmount_GreaterThanCurrentHealth_HealthShouldNotGoBelow0() 
		{
			player.playerHealth = 57f;

			player.PlayerTakesDamage (60f);
			Assert.AreEqual (0f, player.playerHealth);
		}

		[Test]
		public void PlayerPicksUpNewWeapon_EquipPickpedUpWeapon()
		{
			var sword = new Object ();

			player.pickUpWeapon (sword);
			Assert.AreEqual (sword, player.equippedWeapon);
		}

		[Test]
		public void PlayerInteractsWithItem_GiveFeedback_ApplyItemsModifiers()
		{
			var testItem = new Object ();
			player.interactWithItem (testItem);

			//Currently we havent decided how to handle items therefore this test fails.
			Assert.Fail ();
		}


		[Test]
		public void PlayerDrinksHealthPotion_AlreadyAtMaxHealth_HealthShouldNotGoAboveMax()
		{
			var healthPotion = new Object ();
			player.playerHealth = 100f;
			player.interactWithItem (healthPotion);

			//Currently we havent decided how to handle items therefore this test fails.
			Assert.Fail ();
		}

		[Test]
		public void PlayerDrinksHealthPotion_IncreasePlayerHealthBySetAmount()
		{
			var healthPotion = new Object ();

			player.interactWithItem (healthPotion);

			//Currently we havent decided how to handle items therefore this test fails.
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonToViewPlayerStatus_ShowPlayerStatus()
		{
			player.viewPlayerStatus ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonToViewGearStatus_ShowGearStatus()
		{
			player.viewGearStatus ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonToViewItems_ShowItemsScreen()
		{
			player.viewItems ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonToPerformMeleeAttack_PerformMeleeAttack()
		{
			player.performMeleeAttack ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonToPerformRangedAttack_PerformRangedAttack()
		{
			player.performRangedAttack ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonsToPerformComboAttack_PerformComboAttack()
		{
			player.performComboAttack ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerReaches0HP_ShowDeathAnimation()
		{
			player.showDeathAnimation ();
			Assert.Fail ();
		}

		[Test]
		public void PlayerPressesButtonToInteractWithGundamGear_ShowAppropraiteAnimation_AssocatiatedWithInteraction()
		{
			player.interactWithGundamGear ();
			Assert.Fail ();
		}
	}
}

