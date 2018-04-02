using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController	 : MonoBehaviour
{
	/* PlayerController.cs
 *
 * This class is responsible for handling the movement, logic and actions of the player. 
 * 
 */
	[HideInInspector] public Rigidbody2D rb2d;
	[HideInInspector] public bool facingRight = true;
	// the direction the player is facing
	[HideInInspector] public bool jump = false;
	// if the player has jumped
	[HideInInspector] public Transform groundCheck;
	// used for line casting to the ground to check if the player is on the ground
	[HideInInspector] public SpriteRenderer sp2d;
	[HideInInspector] public BoxCollider2D bc2d;

	public float moveForce = 300f;
	// the move force of the player it controls how fast the player moves
	public float jumpForce = 500f;
	// the jump force of the player it controls how high the player jumps
	private bool attacking = false;
	// a condition that checks if the player is attacking
				
	private float h;
	// The input for  left or right from the user
	public static bool grounded = false;
	// checks if the player is on the ground
	private Animator anim;
	// The animator control for player

	//public Vector3 startLocation;

	public Vector2 dashVelocity = new Vector2 (1, 0);
	// controls the speed of the dash;

	public Rigidbody2D bullet;
	// the bullet the player spawns

	public Transform playerLocation;
	// public variable of the players location

	public Transform explosion;
	// The explosion that is created for the player when he dies
	public GameObject dashFlame;
	// The dash flame that appears when the palyer is dashing
	public float fallMultiplier = 2.5f;
	// a variable meant to increase the fall speed after the peak of the player's intial jump
	public float lowJumpMultiplier = 2f;
	// a variable meant to increase the fall speed of the jump after to player has let go of the jump key
	public Attack[] attacks;
	// an array of attack which contain's ground attack's of player
	public Attack dashAttack;
	// attack information for dash attack
	public Attack[] airAttacks;
	// an array of attack which contain's air attack's of player

	public bool inHitStun;
	//check if the player is in hitstun
	public bool dashing = false;
	//check if the player is in hitstun
	public bool inCA;
	//check if the player is in hitstun
	public bool isJumped;
	//check if the player is in hitstun
	public bool inSlowDash;
	//check if the player is in hitstun
	public bool inLoseEnergy;
	//check if the player is in hitstun
	public bool inEnergyReg;
	//check if the player is in hitstun
	public bool inWaitShot;
	//check if the player is in hitstun
	public bool inBurstShot;
	//check if the player is in hitstun
	public bool inHit;
	//check if the player is in hitstun
	public bool inShotCoolDown;
	//check if the player is in hitstun
	public bool inMove;
	//check if the player is in hitstun
	public bool isDead;
	//check if the player is in hitstun
	public bool inComboAttack = false;
	//check if the player is in hitstun
	public bool backdashing = false;
	//check if the player is in hitstun
	public bool crouching;
	//check if the player is in hitstun


	private Transform mp;
	//the transform of a movming platform used so when the player jumps on a moving platform he follow sit

	private int moveCounter;
	private Vector2 pvSize = new Vector2 (0.33f, 0.96f);
	public static int dmgUp = 0;
	public static int def = 0;

/* Player Jump
 *
 * This method is responsible the player jumping
 * 
 */
	public void playerJump ()
	{
		anim.SetTrigger ("Jump");
		transform.SetParent (null);
		FindObjectOfType<SoundManagerScript> ().PlaySound ("jump");
		rb2d.AddForce (new Vector2 (0, jumpForce));
		isJumped = true;
	}

	void Awake ()
	{
		sp2d = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		bc2d = GetComponent<BoxCollider2D> ();
		Application.targetFrameRate = 60;
		GameManager.playerControls = true;
	}

/* flip
 *
 * This method is making the player face the other direction
 * 
 */

	public void flip ()
	{
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		facingRight = !facingRight; 
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

/* controls 
 *
 * This method this removes the controls from the player for a given amount of time
 * 
 */


	public IEnumerator controls (float time)
	{
		GameManager.playerControls = false; 
		yield return new  WaitForSeconds (time);
		GameManager.playerControls = true; 
	}

	void Update ()
	{
		playerLocation = rb2d.transform;
		h = Input.GetAxis ("Horizontal");
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));


		if (GameManager.playerControls) {
			if (!inEnergyReg) {
				StartCoroutine (energyReg (5f));
			}
				
			// if player is on the ground set to ground idle animation
			if (grounded && !inHitStun) {
				anim.SetTrigger ("Grounded"); 
			}


			//if player is in the air set him to jump animation 
			if (!grounded && !attacking && !inHitStun) {
				anim.SetTrigger ("Jump");
			}
			// move player right
			if (h > 0 && !inHitStun && !isDead && !dashing && !crouching && !backdashing && !attacking) {
				transform.SetParent (null); 
				StartCoroutine (dashInput ("Right"));
				walkPlayer ("right");
			}
			// move player left
			if (h < 0 && !inHitStun && !isDead && !dashing && !crouching && !backdashing && !attacking) {
				transform.SetParent (null); 
				StartCoroutine (dashInput ("Left"));
				walkPlayer ("left");
			}
			//set player to idle if not moving
			if (h == 0 && !inHitStun && !isDead) {
				anim.SetTrigger ("Idle");
				rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}
			// player jump
			if (Input.GetButtonDown ("Jump") && grounded && !inHitStun && !isDead && !backdashing) {
				playerJump ();
			}

			// player crouch 
			if (grounded && !inHitStun && Input.GetButtonDown ("Down") && !dashing && !Input.GetButton ("Right") && !Input.GetButton ("Left")) {
				crouching = true;
				anim.SetBool ("Crouch", true);
				bc2d.size = new Vector2 (0.33f, 0.55f);
			}
			// player stop crouches
			if (!inHitStun && Input.GetButtonUp ("Down")) {
				anim.SetBool ("Crouch", false);
				crouching = false;
				bc2d.size = pvSize;
			}
			// player shoot
			if (!inHitStun && Input.GetButtonDown ("Shoot") && GameObject.Find ("ES").GetComponent<Slider> ().value > 1 && !inShotCoolDown && !crouching) {
				StartCoroutine (burstShot (3, 0.1f));
			}

			// player death 
			if (GameObject.Find ("HS").GetComponent<Slider> ().value <= 0 && !isDead) {
				StartCoroutine (death ());
			}
			// player use repair kit
			if (Input.GetButtonDown ("repair") && GameManager.rKits > 0) {
				FindObjectOfType<GameManager> ().useRepairKit ();
			}

			// player will fall quicker after the peak of his jump and if you release the jump button 
			if (rb2d.velocity.y < 0) {
				rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
			} else if (rb2d.velocity.y > 0 && !Input.GetButton ("Jump")) {
				rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
			
			
			}
			//player triple hit attack in the air and ground
			if (Input.GetButtonDown ("Attack1") && !inHitStun && !attacking && !isDead && !backdashing && !inComboAttack) {
				if (dashing) {
					StartCoroutine (playerAttack (dashAttack));
				}
				if (!grounded && !dashing) {
					StartCoroutine (comboAttacks (airAttacks));
				} else if (!dashing) {
					StartCoroutine (comboAttacks (attacks));
				}
			
			}

		}
		// player back dash 
		StartCoroutine (backDash ());

	}


	/* walkPlayer 
 *
 * This method responsible for moving the player. it accepts the input "left" or "right"
 * left moves the player left and right moves the player right
 * 
 */


	public void walkPlayer (string DIRECTION)
	{
		if (DIRECTION == "right") {
			if (!facingRight) {
				flip ();
			}
			anim.SetTrigger ("Walk");
			rb2d.velocity = (new Vector2 (0, rb2d.velocity.y));
			rb2d.AddForce (new Vector2 (moveForce, 0));
		} else if (DIRECTION == "left") {
			if (facingRight) {
				flip ();
			}
			anim.SetTrigger ("Walk");
			rb2d.velocity = (new Vector2 (0, rb2d.velocity.y));
			rb2d.AddForce (new Vector2 (-moveForce, 0));
		}

	}


/* waitShot 
 *
 * This method responsible for creating a bullet and adding delay for shots. 
 * 
 * Input time: delay for each shot
 */

	IEnumerator waitShot (float time)
	{
		inWaitShot = true; 
		Instantiate (bullet, playerLocation);
		FindObjectOfType<SoundManagerScript> ().PlaySound ("rangeAttack");
		FindObjectOfType<GameManager> ().decreaseEnergy (1);
		yield return new WaitForSeconds (time);
		inWaitShot = false; 
	}

	/* burstShot 
 *
 * This method responsible a given numer of shots and they delay in between them  
 * 
 * 
 */

	IEnumerator burstShot (int burstShot, float time)
	{
		inBurstShot = true; 
		anim.SetTrigger ("Shoot");
		StartCoroutine (shotCoolDown (1.5f));
		float n = 0;
		while (n < burstShot) {
			if (!inWaitShot) {
				n++;
				StartCoroutine (waitShot (time));
			}
			yield return null;
		}
		inBurstShot = false; 
	}
	// gives the cooldown between shots 
	IEnumerator shotCoolDown (float time)
	{
		inShotCoolDown = true; 
		yield return new  WaitForSeconds (time);
		inShotCoolDown = false; 
	}
	// responsible for combo attack logic
	IEnumerator comboAttacks (Attack[] attacks)
	{
		inComboAttack = true;
		float delta = 0;
		int moveIndex = 0;
		float[] attackTimelist = new float[attacks.Length + 1];
		attackTimelist [0] = 0;
		foreach (Attack element in attacks) {
			int count = 1;
			attackTimelist [count] = element.cancelTime;
			count++;
		}
		while (inComboAttack) {
			delta += Time.deltaTime;
			if (Input.GetButtonDown ("Attack1") && !attacking && delta > attackTimelist [moveIndex] && inComboAttack) {
				delta = 0;
				if (moveIndex >= (attacks.Length)) {
					break;
				}
				StartCoroutine (playerAttack (attacks [moveIndex]));
				moveIndex++;
			}
			if (delta > 0.6f) {
				inComboAttack = false;
			}
			yield return null;
		}
		inComboAttack = false;
	}
	// gives the player 1 energy for the given time 
	IEnumerator energyReg (float time)
	{
		inEnergyReg = true; 
		FindObjectOfType<GameManager> ().increaseEnergy (1);
		yield return new WaitForSeconds (time);
		inEnergyReg = false;

	}
	// back dash 
	IEnumerator backDash ()
	{
		if (Input.GetButtonDown ("Backdash") && !inHitStun && !attacking && !isDead && grounded) {
			float delta = 0;
			backdashing = true; 
			float d = getDirection (facingRight);
			anim.SetTrigger ("Backdash");
			while (delta < 0.3f) {
				delta += Time.deltaTime;
				float translation = Time.deltaTime * 10;
				rb2d.MovePosition (rb2d.position + -d * dashVelocity * translation);
				yield return null;
			}
		
			backdashing = false; 
		}
		yield return null;
	}
	// player attack lgoc 
	IEnumerator playerAttack (Attack attack)
	{
		attacking = true;
		anim.SetTrigger (attack.animationTrigger);
//		createSlash (slash, attack);
		yield return new WaitForSeconds (attack.startDelayTime);
		FindObjectOfType<SoundManagerScript> ().PlaySound (attack.attackSound);
		attack.attackCollider.enabled = true; 
		yield return new WaitForSeconds (attack.durationTime);
		attack.attackCollider.enabled = false; 
		while (dashing && grounded) {
			if (Mathf.Abs (rb2d.velocity.x) < 0.02f) {
				rb2d.velocity = new Vector2 (0f, rb2d.velocity.y);
				break;
			}
			yield return null;
			if (!inSlowDash) {
				dashing = false;
				anim.SetBool ("Dash", false);
				dashFlame.SetActive (false);
				StartCoroutine (controls (0.6f));
				StartCoroutine (slowDashAttack ());
			}
		}
		attacking = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Spike")) {
			getHit (0.5f, 25, new Vector2 (0, 0));
		}
		if (other.gameObject.CompareTag ("Flag")) {
			FindObjectOfType<GameManager> ().nextLevel ();
		}
		if (other.gameObject.CompareTag ("eAttack")) {
			getHit (0.5f, 5, new Vector2 (0, 0));
		}
		if (other.gameObject.CompareTag ("Flame")) {
			getHit (0.5f, 5, new Vector2 (0, 0));
		}
		if (other.gameObject.CompareTag ("Enemy") && !inHit) {
			inHit = true;
			getHit (0f, 5, new Vector2 (0, 0));
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			inHit = false;
		}
	}
	// slow player's speed when he dash attacks
	public IEnumerator slowDashAttack ()
	{
		inSlowDash = true;
		rb2d.velocity = new Vector2 (rb2d.velocity.x / 20f, rb2d.velocity.y);
		yield return new WaitForSeconds (0.1f);
		inSlowDash = false; 
	}

	// player death
	public IEnumerator death ()
	{
		isDead = true;
		anim.SetTrigger ("Hurt");
		Instantiate (explosion, playerLocation);
		rb2d.simulated = false;
		FindObjectOfType<SoundManagerScript> ().PlaySound ("playerHit");
		yield return new WaitForSeconds (1);
		FindObjectOfType<GameManager> ().playDeath ();
	}
	// player hit stun 
	public IEnumerator waitForHitstun (float hitStun)
	{
		inHitStun = true;
		anim.SetTrigger ("Hurt");
		foreach (Attack hitBox in attacks) {
			hitBox.attackCollider.enabled = false;
		}
		FindObjectOfType<SoundManagerScript> ().PlaySound ("takeDamage");
		StartCoroutine (blink ());
		yield return new WaitForSeconds (hitStun);
		inHitStun = false;
	}
	// wait for double tab
	public IEnumerator dashInput (string direction)
	{
		if (Input.GetButtonUp (direction) && !dashing) {
			float delta = 0; 
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				if (Input.GetButtonDown (direction) && !dashing) {
					StartCoroutine (playerDash (direction));
				}
				yield return null;

			}
		}
	}
	// dash palyer
	public IEnumerator playerDash (string direction)
	{
		dashing = true;
		float d = 0f;
		if (direction == "Right") {
			d = 1f;
		}
		if (direction == "Left") {
			d = -1f;
		}
		while (dashing) {
			dashFlame.SetActive (true);
			if (!inLoseEnergy) {
				//StartCoroutine (loseEnergy (0.1f));
			}
			anim.SetBool ("Dash", true);
			rb2d.velocity = (new Vector2 (10f * d, rb2d.velocity.y));
			if (Input.GetButtonUp (direction)) {
				dashFlame.SetActive (false);
				anim.SetBool ("Dash", false);
				dashing = false;

				StartCoroutine (slowDashAttack ());
			}

			yield return null;
		}

		yield return null;
	}
	// lose energy
	public IEnumerator loseEnergy (float time)
	{
		inLoseEnergy = true;
		FindObjectOfType<GameManager> ().decreaseEnergy (2);
		yield return new WaitForSeconds (time);
		inLoseEnergy = false; 
	}

	// used for visual damage indicator
	public IEnumerator blink ()
	{
		sp2d.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		sp2d.color = Color.white;
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.collider.CompareTag ("Fire Dino")) {
			getHit (0f, 5, new Vector2 (0, 0));
		}
		if (other.collider.CompareTag ("Enemy")) {
			Physics2D.IgnoreCollision (other.collider, bc2d);
		}
		if (other.collider.CompareTag ("RepairKit") && !isDead) {
			FindObjectOfType<GameManager> ().increaseRkits ();

		}
		if (other.collider.CompareTag ("Death") && !isDead) {
			getHit (0f, 100, new Vector2 (0, 0));
		}

		if (other.gameObject.CompareTag ("mp")) {
			print ("mp");
			mp = other.gameObject.transform; 
			transform.SetParent (mp);
		}
	}

	void OnCollsionExit2D (Collision2D other)
	{
		if (other.gameObject.CompareTag ("mp")) {
			transform.SetParent (null);  
		}
	}
	// transform the bool direction of the player to a float 
	public float getDirection (bool direction)
	{
		float s;
		if (direction) {
			s = -1.0f;
		} else {
			s = 1.0f;
		}
		return -s;
	}
	// player take damage
	public void getHit (float hitstun, int damage, Vector2 hitForce)
	{
		Slider[] hs = GameObject.Find ("HS").GetComponents<Slider> ();
		FindObjectOfType<GameManager> ().removeHealth (hs, damage);
		rb2d.velocity = hitForce;
		StartCoroutine (waitForHitstun (hitstun));

	}
	// move player to a point
	public IEnumerator move (Vector3 target, float ltime)
	{
		inMove = true; 
		anim.SetBool ("hover", true);
		float time = 0;
		while (time < ltime) {
			yield return null;
			time += Time.deltaTime;
			print (time);
			float step = 3 * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target, step);
		}
		anim.SetBool ("hover", false);
		inMove = false;
	}
		
}