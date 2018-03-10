using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyControllerTest {
	public Vector2 rb2dVelocity;
	public PlayerController playercontroller;
	public float moveForce = 100f;
	public int EnemyCount;
	private float hitForce = 200f;
	public int animFloatSpeed=1;
	public float health = 100;
	public bool isDead = false;
	public bool attacking = false;
	public bool facingRight = true;
	public bool inHitStun = false;
	public float enemyDistance = 50f;
	public float enemyNear = 2.5f;
	public float delta;
	public BoxCollider2D attack;
	private bool inLauncher = false;
	public float deltaY;
	// setup similar variable as base class.

	public bool isActive = true;
	public Vector3 localScale = new Vector3 (1, 1, 1);
	public string animTrigger;
	public string soundScript;
	public int scrapCount = 0;
	public Vector2 playerVelocity = new Vector2(10,0);
	public float timeWaited = 0;


	public void flip ()
	{
		facingRight = !facingRight; 
		Vector3 theScale = localScale; //tjeScale is temporary variable
		theScale.x *= -1;
		localScale = theScale;
	}

	[Test]
	public void flipTest ()
	{
		flip ();
		Assert.AreEqual(localScale, new Vector3(-1, 1, 1));
	}

	public void enemyMoveRight ()
	{
		if (!attacking) {
			animFloatSpeed = 1;
		}
		rb2dVelocity = new Vector2 (0, rb2dVelocity.y);
		rb2dVelocity += new Vector2 (moveForce, 0);
	}

	[Test]
	public void enemyMoveRightTest(){
		attacking = true;
		enemyMoveRight ();
		Assert.AreEqual (rb2dVelocity, new Vector2 (100f, 0)); //Test change in movement velocity
		attacking = false;
		enemyMoveRight ();
		Assert.AreEqual (animFloatSpeed, 1);
	}

	public void enemyMoveLeft ()
	{
		if (!attacking) {
			animFloatSpeed = 1;
		}
		rb2dVelocity = new Vector2 (0, rb2dVelocity.y);
		rb2dVelocity += new Vector2 (-moveForce, 0);
	}

	[Test]
	public void enemyMoveLeftTest(){
		attacking = true;
		enemyMoveLeft ();
		Assert.AreEqual (rb2dVelocity, new Vector2 (-100f, 0)); //Test change in movement velocity
		attacking = false;
		enemyMoveRight ();
		Assert.AreEqual (animFloatSpeed, 1);
	}

	public void death ()
	{
		animTrigger="Death";
		soundScript = "enemyDeath";
		scrapCount += 53;
		isActive = false;
		isDead = true;
	}

	[Test]
	public void deathTest()
	{
		int currentCount = scrapCount;
		Assert.AreNotEqual (animTrigger, "Death");			// verify condition before death
		Assert.AreNotEqual (soundScript, "enemyDeath");
		Assert.AreEqual (scrapCount, 0);
		Assert.IsTrue (isActive);
		Assert.IsFalse (isDead);
		death ();
		Assert.AreEqual (animTrigger, "Death");			// verify condition before death
		Assert.AreEqual (soundScript, "enemyDeath");
		Assert.AreEqual (scrapCount, 53);
		Assert.IsFalse (isActive);
		Assert.IsTrue (isDead);
	}

	void OnCollisionEnter2D (string collisionTag)
	{
		if (collisionTag == "player") {
			playerVelocity = new Vector2 (0, 0);
		}  
	}

	[Test]
	public void OnColissionEnter2DTest () {
		OnCollisionEnter2D ("player");
		Assert.AreEqual (playerVelocity, new Vector2 (0, 0));	// verify change of velocity
	}

	public void waitForHitstun ()
	{
		inHitStun = true;
		timeWaited += 2;
		if (!inLauncher) {
			inHitStun = false;
		}
	}

	[Test]
	public void waitForHitstunTest() {
		timeWaited = 0;
		waitForHitstun ();
		Assert.IsFalse (inHitStun);
		Assert.AreEqual (timeWaited, 2);
	}

	public void waitForHitstunL ()
	{
		inHitStun = true;
		inLauncher = true; 
		timeWaited += 5;
		inLauncher = false; 
		inHitStun = false;
	}

	[Test]
	public void waitForHitstunLTest() {
		timeWaited = 0;
		waitForHitstunL ();
		Assert.IsFalse (inHitStun);
		Assert.AreEqual (timeWaited, 5);
	}

	public void Attack ()
	{
		attacking = true;
		soundScript = "attack";
		animTrigger = "Attack";
		attacking = true;
		timeWaited = 0.5f;
		attacking = false;
		attacking = false;
	}

	[Test]
	public void AttackTest() {
		Attack ();
		Assert.AreEqual (soundScript, "attack");
		Assert.AreEqual (animTrigger, "Attack");
		Assert.AreEqual (timeWaited, 0.5f);
	}
}
