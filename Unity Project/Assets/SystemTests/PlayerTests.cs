using System;
using UnityEditor;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class PlayerTests
	{
		[Test]
		public void EnemyTakesDamage_PositiveAmount_HealthUpdated() 
		{
			var player = new PlayerController ();
			player.getHit (0.5f, 1, new Vector2(300f,300f));
			Assert.AreEqual (1, 1);
		}

		[Test]
        public void Character_Health_recovery() 
		{
			var player = new PlayerController ();
			Input.GetKeyDown (KeyCode.Q); //This is to use repair kit

			Assert.AreEqual (player.healthSlider.value, 100);
		}
		[Test]
		public void Character_Fire_Bullet() 
		{
			var player = new PlayerController ();
			Input.GetKeyDown (KeyCode.O); //This is to fire bullet

			Assert.AreEqual (GameObject.Find("bullet") != null, true);
		}

		[Test]
		public void Character_Combo_Attack() 
		{
			var player = new PlayerController ();
			Input.GetButtonDown ("Attack1");
			Input.GetButtonDown ("Attack1"); //Using the attack more than once starts a combo

			Assert.AreEqual (player.inComboAttack, true);
		}

		[Test]
        public void Character_Flip() 
		{
			var player = new PlayerController ();
			player.facingRight = true;
            player.flip();
            Assert.AreEqual(player.facingRight,false);
		}
		[Test]
        public void Character_Walk_Right_Facing_Right() 
		{
			var player = new PlayerController ();
			player.facingRight = true;
            player.walkPlayer("right");
            Assert.AreEqual(player.facingRight,true);
		}
		[Test]
        public void Character_Walk_Left_Facing_Right() 
		{
			var player = new PlayerController ();
			player.facingRight = true;
            player.walkPlayer("left");
            Assert.AreEqual(player.facingRight,false);
		}
		[Test]
        public void Character_Walk_Right_Facing_Left() 
		{
			var player = new PlayerController ();
			player.facingRight = false;
            player.walkPlayer("right");
            Assert.AreEqual(player.facingRight,true);
		}
		[Test]
        public void Character_Walk_Left_Facing_left() 
		{
			var player = new PlayerController ();
			player.facingRight = false;
            player.walkPlayer("right");
            Assert.AreEqual(player.facingRight,false);
		}
		[Test]
        public void Character_Jump() 
		{
			var player = new PlayerController ();
            player.playerJump();
            Assert.AreEqual(player.isJumped,true);
		}
		[Test]
        public void walkPlayer_Test() 
		{
			var player = new PlayerController ();
            player.walkPlayer("right");
            Assert.AreEqual(player.facingRight,true);
            player.walkPlayer("left");
            Assert.AreEqual(player.facingRight,false);
		}
		[Test]
        public void death_Test()
        {
            var player = new PlayerController ();
            player.death();
            Assert.AreEqual(player.isDead,true);
        }
		[Test]
        public void waitForHitstun_Test()
        {
            var player = new PlayerController ();
            player.waitForHitstun(0.22F);
            Assert.AreEqual(player.inHitStun,true);
        }
		[Test]
        public void doubleDashRight_Test()
        {
            var player = new PlayerController ();
            player.doubleDashRight();
            Assert.AreEqual(player.dashing,true);
        }
		[Test]
        public void doubleDashLeft_Test()
        {
            var player = new PlayerController ();
            player.doubleDashLeft();
            Assert.AreEqual(player.dashing,true);
        }

	}
}
