using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyControllerTest {
	private Rigidbody2D rb2d;
	public PlayerController playercontroller;
	public float moveForce = 100f;
	public int EnemyCount;
	private float hitForce = 200f;
	private Animator anim;
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

	public Vector3 localScale = new Vector3 (1, 1, 1);

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

	[Test]

}
