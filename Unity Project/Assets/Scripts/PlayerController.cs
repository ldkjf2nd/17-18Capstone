using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController	 : MonoBehaviour {
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false; 
	private Vector2 stop = new Vector2 (0, 0);
	public float hitForce = 300f;
	private float moveForce = 300f; 
	private float maxSpeed = 30f;
	public float jumpForce = 500f;
	private int direction;
	public bool attacking = false;
	public Transform groundCheck; 
	public Collider2D attack;
    public float h; 
	private int boostMeter = 100;
	public static bool grounded = false;
	public Animator anim;
	public Rigidbody2D rb2d;
	public Vector3 startLocation;
	public bool inHitStun;
	public bool dashing = false;
	public Vector2 dashVelocity = new Vector2 (1, 0);
	public bool backdashing = false;
	public bool crouching = true;
	public Rigidbody2D bullet;
	public bool inComboAttack = false;
	public Slider healthSlider; 
	public Transform playerLocation;
	public bool isDead;
	public Transform explosion; 
	public Slider energySlider;
	public GameObject dashFlame;
	public float fallMultiplier = 2.5f; 
	public float lowJumpMultiplier = 2f;
	public Attack[] attacks;
	public Attack attack1;
	public bool inCA;
    public bool isJumped;
 
	public SpriteRenderer sp2d;
	public BoxCollider2D bc2d;
	private Vector2 pvSize = new Vector2 (0.33f, 0.96f); // player hitbox 
	public static int  dmgUp = 0;
	public static int  def = 0; 
	public void intilization(){
		sp2d = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		bc2d = GetComponent<BoxCollider2D> ();
		rb2d.simulated = true;
		rb2d.freezeRotation = true;
		rb2d.mass = 1;
		rb2d.tag = "Player";
		inComboAttack = false;
		dashFlame.SetActive(false);
		Application.targetFrameRate = 60;
		attacking = false;
		isDead = false;
		bc2d.size = pvSize;
	
	
	}


	public void Constructor(Object rb2d, float jumpForce){
		jumpForce = jumpForce; 
		rb2d = rb2d;
	}
		
	public void playerJump(){
		anim.SetTrigger ("Jump");
		FindObjectOfType<SoundManagerScript> ().PlaySound ("jump");
		rb2d.AddForce(new Vector2(0, jumpForce));
        isJumped = true;
	}
	void Awake () {
		intilization ();
	}
	public void flip (){
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		facingRight = !facingRight; 
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

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

	void Update(){
		if (GameManager.playerControls) {
			playerLocation = rb2d.transform;
			h = Input.GetAxis ("Horizontal");
			grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
			if (grounded && !inHitStun) {
			anim.SetTrigger ("Grounded"); 
			}
			if (!grounded && !attacking && !inHitStun ) {
				anim.SetTrigger ("Jump");
			}
			if (h > 0 &&!inHitStun && !isDead && !dashing &&!crouching && !backdashing && !attacking) {
				walkPlayer ("right");
			}

			if (h < 0 &&!inHitStun && !isDead && !dashing &&!crouching && !backdashing && !attacking) {
				walkPlayer ("left");
			}
			if (h == 0 &&!inHitStun && !isDead) {
			anim.SetTrigger ("Idle");
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}

			if (Input.GetButtonDown ("Jump") && grounded &&!inHitStun && !isDead && !backdashing) {
			playerJump ();
			}

			if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) ){
			anim.SetBool("Dash",false);
			dashing = false;
			dashFlame.SetActive(false);
		}

		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.S) && !dashing) {
			crouching = true;
			anim.SetBool("Crouch",true);
			bc2d.size = new Vector2(0.33f, 0.55f);
		}
		if (!inHitStun && Input.GetKeyUp  (KeyCode.S)) {
			anim.SetBool("Crouch",false);
			crouching = false;
			bc2d.size = pvSize;
		}
		if (!inHitStun && Input.GetKeyUp (KeyCode.O) && energySlider.value > 1) {
			anim.SetTrigger ("Shoot");
			FindObjectOfType<SoundManagerScript> ().PlaySound ("rangeAttack");
			fire ();
		}
		if (healthSlider.value <= 0 && !isDead) {
			StartCoroutine (death());
		}
		if (Input.GetKeyDown (KeyCode.Q)&& GameManager.rKits > 0) {
				GameManager.rKits -= 1;
				FindObjectOfType<SoundManagerScript> ().PlaySound ("repair");
				healthSlider.value = 100;
		}
			if (rb2d.velocity.y < 0) {
				rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
			}
			else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump")){
				rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
			
			
			}

			if (Input.GetButtonDown ("Attack1") &&!inHitStun && !attacking && !isDead && !dashing  && !backdashing &&!inComboAttack) {
				StartCoroutine(comboAttacks (attacks));
			}






		StartCoroutine (doubleDashRight ());
		StartCoroutine (doubleDashLeft ());
		StartCoroutine (backDash ());
//StartCoroutine (comboAttack());




		}

	}
	void fire(){
		energySlider.value -= 1;
		Instantiate (bullet, playerLocation);
	}

	IEnumerator comboAttacks(Attack[] attacks){
		inComboAttack = true;
		float delta = 0;
		int moveIndex = 0;
		float[] attackTimelist= new float[attacks.Length+1];
		attackTimelist [0] = 0;
		foreach (Attack element in attacks) {
			int count = 1;
			attackTimelist [count] = element.cancelTime;
			count++;
		}
		while (inComboAttack) {
			delta += Time.deltaTime;
			if (Input.GetButtonDown ("Attack1") && !attacking && delta > attackTimelist[moveIndex] && inComboAttack) {
				delta = 0;
				if (moveIndex >= (attacks.Length)) {
					break;
				}
				StartCoroutine (playerAttack (attacks[moveIndex]));
				moveIndex++;
			}
			if (delta > 0.6f) {
				inComboAttack = false;
			}
			yield return null;
		}
		inComboAttack = false;
	}
	IEnumerator backDash(){
		if (Input.GetButtonDown("Backdash")&&!inHitStun &&!attacking &&!isDead &&grounded ){
			float delta = 0;
			backdashing = true; 
			float d = getDirection (facingRight);
			anim.SetTrigger("Backdash");
			while(delta < 0.3f){
				delta += Time.deltaTime;
				float translation = Time.deltaTime * 10;
				rb2d.MovePosition(rb2d.position + -d*dashVelocity*translation);
			yield return null;
			}
		
			backdashing = false; 
		}
		yield return null;
	}

	IEnumerator playerAttack(Attack attack){
		attacking = true;
		anim.SetTrigger (attack.animationTrigger);
		yield return new WaitForSeconds (attack.startDelayTime);
		FindObjectOfType<SoundManagerScript> ().PlaySound (attack.attackSound);
		attack.attackCollider.enabled = true; 
		yield return new WaitForSeconds (attack.durationTime);
		attack.attackCollider.enabled = false; 
		attacking = false;
	}
		
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Spike")){
			healthSlider.value -= 100;
		}
		if (other.gameObject.CompareTag ("Flag")) {
			FindObjectOfType<GameManager> ().nextLevel ();
		}
		if (other.gameObject.CompareTag ("eAttack")) {
			getHit (0.5f, 1, new Vector2(hitForce,hitForce));
		}

	}
	public IEnumerator death(){
		isDead = true;
		anim.SetTrigger ("Hurt");
		Instantiate (explosion, playerLocation);
		rb2d.simulated = false;
		FindObjectOfType<SoundManagerScript> ().PlaySound ("playerHit");
		yield return new WaitForSeconds (1);
		FindObjectOfType<GameManager> ().playDeath ();
	}
	public IEnumerator waitForHitstun(float hitStun){
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
	public IEnumerator doubleDashRight(){
		if (Input.GetKeyUp (KeyCode.D) && facingRight && grounded &&!inHitStun &&!attacking &&!isDead &&!crouching && !backdashing) {
			float delta = 0;
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				if (Input.GetKeyDown (KeyCode.D)) {
					dashing = true;
					dashFlame.SetActive(true);
					anim.SetBool ("Dash",true);
					rb2d.AddForce (new Vector2 (moveForce*2, 0));
					if(Input.GetKeyUp(KeyCode.D)){
						dashing = false;
					}
				
				}
				yield return null;
			}
		}
		yield return null;
	}
	public IEnumerator doubleDashLeft(){
		if (Input.GetKeyUp (KeyCode.A) && !facingRight && grounded &&!inHitStun &&!attacking &&!isDead &&!crouching && !backdashing) {
			float delta = 0;
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				if (Input.GetKeyDown (KeyCode.A)) {
					dashing = true;
					anim.SetTrigger ("Dash");
					dashFlame.SetActive(true);
					rb2d.AddForce (new Vector2 (-moveForce*2, 0));
					if(Input.GetKeyUp(KeyCode.D)){
						dashing = false;
					}

				}
				yield return null;
			}
		}
		yield return null;
	}
	public IEnumerator blink(){
		sp2d.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		sp2d.color = Color.white;
	}

	void OnCollisionEnter2D(Collision2D other){
		
		if (other.collider.CompareTag("Enemy") &&!isDead) {
			getHit (0.5f, 1, new Vector2(hitForce,hitForce));
		}
		if (other.collider.CompareTag ("Fire Dino") &&!isDead) {
			getHit (0.5f, 10, new Vector2(hitForce,hitForce));
		} 
		if (other.collider.CompareTag ("RepairKit") &&!isDead) {
			FindObjectOfType<GameManager>().increaseRkits ();

		}





	}
	float getDirection(bool direction){
		float s;
		if (direction) {
			s = -1.0f;
		} else {
			s = 1.0f;
		}
		return -s;
	}

	public void getHit(float hitstun, int damage, Vector2 hitForce){
		healthSlider.value -= damage;
		rb2d.AddForce (hitForce);
		StartCoroutine (waitForHitstun (hitstun));

	}
		
}