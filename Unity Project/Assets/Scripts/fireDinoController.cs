using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireDinoController : MonoBehaviour {
	private Rigidbody2D rb2d;
	public Animator anim;
	private float jumpForce = 7500f;
	private float horizontalForce = 5000f;
	public bool onGround = false;
	public bool isDead = false;
	public Transform groundCheck;
	public float t;
	public float delta;
	public bool inHitStun;
	public bool facingRight;
	public Rigidbody2D bullet;
	public Rigidbody2D homingShot;
	public float deltaY;
	public Animator fire;
	public Slider healthSlider;
	public GameObject healthHUD; 
	public SpriteRenderer sp2d;
	public BoxCollider2D[] hurtBox;
	public bool canShoot;
	public bool wallAttacking;
	public bool seqDone;
	public bool wallChargeDone;
	public Vector3 lastPosition;
	public string[] moveCycle;
	public int moveStage;
	public bool inFireBallAttack;
	public bool inJumpAttack;
	public bool inChargeAttack;
	public bool jumpingToWall;
	public bool onWall;
	public bool inWallCharge;
	public bool inIntro;
	public bool doneIntro;
	public bool inDeath;
	public string[] wallSequence;
	public int wallStage;
	public Transform explosion;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();	
		sp2d = GetComponent<SpriteRenderer> ();
		hurtBox = GetComponents<BoxCollider2D> ();
		hurtBox [0].offset = new Vector2 (0f, -0.08f);
		hurtBox [0].size = new Vector2 (0.46f, 0.4f);
		hurtBox [1].offset = new Vector2 (-0.38f, 0.04f);
		hurtBox [1].size = new Vector2 (0.41f, 0.09f);
		hurtBox [2].offset = new Vector2 (-0.15f, 0.18f);
		hurtBox [2].size = new Vector2 (0.49f, 0.21f);
		lastPosition = new Vector3(0f,0f,0f);
		seqDone = false;
		onWall = false;
		t = Time.deltaTime;
		facingRight = false;
		moveStage = 0;
		moveCycle = new string[] { "fireBall Attack", "jumpAttack", "ChargeAttack" };
		wallSequence = new string[]{ "jumpToWall", "wallFire Attack", "flame Charge" };
		rb2d.simulated = false; 
		anim.SetTrigger ("WallHug");

	}
	void Update(){
		if (BossTrigger.bossStart) {
			onGround = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
			GameObject thePlayer2 = GameObject.Find ("GW");
			PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();
			delta = Mathf.Abs (playercontroller2.rb2d.position.x - rb2d.position.x);
			deltaY = Mathf.Abs (playercontroller2.rb2d.position.y - rb2d.position.y);
			float step = 10 * Time.deltaTime;
			t += Time.deltaTime;
			if (!doneIntro && !inIntro) {
				StartCoroutine (bossIntro ());
			}
			if (wallStage < 3) {
				if (wallSequence [wallStage % 3] == "jumpToWall" && healthSlider.value < 50 && !jumpingToWall && !wallAttacking && !inWallCharge && !inIntro ) {
					StartCoroutine (jumpToWall ());
					wallStage++;
				}
				if (wallSequence [wallStage % 3] == "wallFire Attack" && onWall && !wallAttacking && !inWallCharge  && !inIntro && !inFireBallAttack) {
					StartCoroutine (wallAttack ());
					wallStage++;
				}
				if (wallSequence [wallStage % 3] == "flame Charge" && onWall && !wallAttacking && !inWallCharge  && !inIntro && !inFireBallAttack) {
					StartCoroutine (wallCharge ());
					wallStage++;
				}
			}	

			if (moveCycle [moveStage % 3] == "fireBall Attack" && !inFireBallAttack && !inJumpAttack && !inChargeAttack && !onWall && !wallAttacking && !inIntro && !inDeath) {
				StartCoroutine (tripleFlameShot ());
				moveStage++;
			}
			if (moveCycle [moveStage % 3] == "jumpAttack" && !inFireBallAttack && !inJumpAttack && !inChargeAttack && !onWall && !wallAttacking && !inIntro && !inDeath) {
				StartCoroutine (jumpAttack ());
				moveStage++;
			}
			if (moveCycle [moveStage % 3] == "ChargeAttack" && !inFireBallAttack && !inJumpAttack && !inChargeAttack && !onWall && !wallAttacking && !inIntro && !inDeath) {
				StartCoroutine (chargeAttack ());
				moveStage++;
			} 


			if ((playercontroller2.rb2d.position.x > rb2d.position.x) && !inHitStun && onGround && !inChargeAttack) {
				if (!facingRight) {
					flip ();
				}
			}
			if (playercontroller2.rb2d.position.x < rb2d.position.x && !inHitStun && onGround && !inChargeAttack) {
				if (facingRight) {
					flip ();
				}
			} 
	
			if (onGround) {
				anim.SetBool ("Jump", false);
				setGroundHitBox ();
			}
			if (!onGround) {
				anim.SetBool ("Jump", true);
				setJumpHitBox ();
			}
		}
	}
	void flameCharge(float d){
		anim.SetTrigger ("Fire Charge");
		FindObjectOfType<SoundManagerScript> ().PlaySound ("fireShot");
		rb2d.velocity = new Vector2 (15*d, 0f);
	}
	void bossFireAttack(){
		anim.SetTrigger ("Attack");	
		FindObjectOfType<SoundManagerScript> ().PlaySound ("fireShot");
		Instantiate (bullet, rb2d.transform);
	}
	void wallFireAttack(){
		anim.SetTrigger ("WallAttack");	
		FindObjectOfType<SoundManagerScript> ().PlaySound ("fireShot");
		Instantiate (homingShot, rb2d.transform);
	}

	public void bossJumpAttack(float direction){
		rb2d.velocity = (new Vector2 (0, 0));
		rb2d.AddForce(new Vector2(horizontalForce*direction, jumpForce));
	}
	void flip (){
			facingRight = !facingRight; 
			Vector3 theScale = transform.localScale; //teScale is temporary variable
			theScale.x *= -1;
			transform.localScale = theScale;
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Attack")|| coll.gameObject.CompareTag ("Attack2")|| coll.gameObject.CompareTag ("Launcher") ){
			healthSlider.value -= 1;
			healthHUD.SetActive (true);
			StartCoroutine (blink ());
			FindObjectOfType<GameManager> ().increaseEnergy (5);
			if (healthSlider.value <= 0) {
				StartCoroutine (death ());
			}
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Wall") || other.gameObject.CompareTag("Player") && inChargeAttack) {
			anim.SetTrigger ("Idle");
			rb2d.velocity = new Vector2 (0, 0);
		}
			
		
	}
	IEnumerator blink(){
		sp2d.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		sp2d.color = Color.white;
	}
	IEnumerator death(){
		inDeath = true; 
		anim.SetTrigger ("Death"); 
		Instantiate (explosion, rb2d.transform);
		healthHUD.SetActive (false);
		yield return new WaitForSeconds (3);
		FindObjectOfType<GameManager> ().resetRespawn ();
		FindObjectOfType<GameManager> ().nextLevel ();
		rb2d.gameObject.SetActive (false);
		inDeath = false;
	}
	void setGroundHitBox(){
		hurtBox [0].offset = new Vector2 (0f, -0.08f);
		hurtBox [0].size = new Vector2 (0.46f, 0.4f);
		hurtBox [1].offset = new Vector2 (-0.38f, 0.04f);
		hurtBox [1].size = new Vector2 (0.41f, 0.09f);
		hurtBox [2].offset = new Vector2 (-0.15f, 0.18f);
		hurtBox [2].size = new Vector2 (0.49f, 0.21f);
	}
	void setJumpHitBox(){
		hurtBox [0].offset = new Vector2 (0.21f, 0.04f);
		hurtBox [1].offset = new Vector2 (-0.32f, 0.38f);
		hurtBox [2].offset = new Vector2 (0.14f, 0.34f);
	}

	IEnumerator jumpAttack(){
		inJumpAttack = true;
		for (int i = 0; i < 3; i++) {
			float d = getDirection (facingRight);
			bossJumpAttack (d);
			yield return new WaitForSeconds (5f); 
		}
		inJumpAttack = false;
	}

	IEnumerator chargeAttack(){
		inChargeAttack = true;
		float d = getDirection (facingRight);
		flameCharge (d);
		yield return new WaitForSeconds (5f); 
		inChargeAttack = false;
	}
		
	IEnumerator tripleFlameShot(){
		inFireBallAttack = true;
		for (int i = 0; i < 3; i++) {
			bossFireAttack ();
			yield return new WaitForSeconds (1f); 
		}
		inFireBallAttack = false;
	}
	IEnumerator wallAttack(){
			wallAttacking = true;
			for (int i = 0; i < 6; i++) {
				wallFireAttack ();
				yield return new WaitForSeconds (1f); 
			}
			wallAttacking = false;
		}
	IEnumerator jumpToWall(){	
		jumpingToWall = true;
		while(rb2d.simulated){
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (226.9f, 30f, -5f), 10 * Time.deltaTime);
			if (transform.position.x > 225f && transform.position.y > 29f) {
				transform.localScale = new Vector3 (7f, 7f, 1f);
				facingRight = false;
				anim.SetTrigger("WallHug");
				rb2d.simulated = false;
				onWall = true;
		}
		yield return null;
		jumpingToWall = false;
		}
		
	}

	IEnumerator wallCharge(){
		inWallCharge = true;
		anim.SetTrigger ("WallJumpAttack");
		rb2d.simulated = true;
		float step = 10 * Time.deltaTime;
		while(deltaY > 1f) {
			GameObject thePlayer2 = GameObject.Find ("GW");
			PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();
			lastPosition = playercontroller2.transform.position;
			transform.position = Vector3.MoveTowards (transform.position, lastPosition, step);
			yield return null;
		} 
		anim.SetTrigger ("Idle");
		transform.position = Vector3.MoveTowards (transform.position, lastPosition, step);
		inWallCharge = false;
		onWall = false;	
		
	}
	IEnumerator bossIntro(){
		inIntro = true;
		rb2d.simulated = true;
		transform.position = Vector3.MoveTowards (transform.position, new Vector3 (224f, 42f, -5f), 1000 * Time.deltaTime);
		anim.SetTrigger ("WallJumpAttack");
		yield return new WaitForSeconds (5f); 
		doneIntro = true;
		inIntro = false;	
	}
		
	float getDirection(bool direction){
		if (direction) {
			return 1f;
		} else {
			return -1f;
		}
	}

}
