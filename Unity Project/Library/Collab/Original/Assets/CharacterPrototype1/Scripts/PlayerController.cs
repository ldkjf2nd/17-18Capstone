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
	private float jumpForce = 500f;
	private int direction;
	private bool attacking = false;
	public Transform groundCheck; 
	public Collider2D attack;
    public float h; //horizontal input index
	private int boostMeter = 100;
	//public Transform boostBar;
	//public int health = 100;
	public static bool grounded = false;
	public Animator anim;
	public Rigidbody2D rb2d;
    public Controller Controller;
	public Vector3 startLocation = new Vector3(0,0,0);
	public bool inHitStun;
	public bool canDash = false;
	public bool dashing = false;
	public Vector2 dashVelocity = new Vector2 (1, 0);
	public bool backdashing = false;
	public bool crouching = true;
	public Rigidbody2D bullet;
	public bool inComboAttack = false;
	public Collider2D attack2;
	public Collider2D attack3;
	public Collider2D attack4;
	public Collider2D jumpAttack;
	public Slider healthSlider; 
	public static int level =1;
	public Transform wallCheck;
	public Transform playerLocation;

	public void intilization(){
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.position = startLocation;
		rb2d.simulated = true;
		rb2d.freezeRotation = true;
		rb2d.mass = 1;
		rb2d.tag = "Player";
		inComboAttack = false;
		//rb2d.collisionDetectionMode.Continuous = true; 
	
	}

	public void playerMoveRight(){
		if (!attacking) {
			anim.SetTrigger ("Walk");
		}
		if (dashing) {
			rb2d.velocity = (new Vector2 (0, rb2d.velocity.y));
			rb2d.AddForce (new Vector2 (moveForce*2, 0));
			return;
		}
		rb2d.velocity = (new Vector2 (0, rb2d.velocity.y));
		rb2d.AddForce (new Vector2 (moveForce, 0));
	}

	public void playerMoveLeft(){
		if (!attacking ) {
			anim.SetTrigger ("Walk");
		}
		if (dashing) {
			rb2d.velocity = (new Vector2 (0, rb2d.velocity.y));
			rb2d.AddForce (new Vector2 (-moveForce*2, 0));
			return;
		}
		rb2d.velocity = (new Vector2 (0, rb2d.velocity.y));
		rb2d.AddForce (new Vector2 (-moveForce, 0));
	}

	public void playerJump(){
		anim.SetTrigger ("Jump");
		rb2d.AddForce(new Vector2(0, jumpForce));
	}

	void Awake () {
		intilization ();
		StartCoroutine (boost ());
        Controller = new Controller();
	}

	void flip (){
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		facingRight = !facingRight; 
		Vector3 theScale = transform.localScale; //teScale is temporary variable
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	// Update is called once per frame
	void Update(){
		playerLocation = rb2d.transform;
		h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (h));
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (grounded && !inHitStun) {
			anim.SetTrigger ("Grounded"); 
		}
		if (!grounded && !attacking && !inHitStun) {
			anim.SetTrigger ("Jump");
		}
		if (h > 0 &&!inHitStun) {
			if (!facingRight) {
				flip ();
			}
			playerMoveRight ();
		}

		if (h < 0 &&!inHitStun) {
			if (facingRight) {
				flip ();
			}
			playerMoveLeft ();
		}

		if (h == 0 &&!inHitStun) {
			anim.SetTrigger ("Idle");
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}

		if (Input.GetButtonDown ("Jump") && grounded &&!inHitStun) {
			playerJump ();
		}

		/* if (Input.GetButton ("Jump") && !grounded &&!inHitStun) {
			if (boostMeter > 0) {
				boostMeter -= 10;
				rb2d.AddForce (new Vector2 (rb2d.velocity.x, 20f));
			}
		} */

		if (Input.GetKeyDown (KeyCode.J) &&!inHitStun && grounded && !dashing) {
			if (!attacking) {
				anim.SetTrigger ("Attack");
				StartCoroutine (Attack ());
			};
		}
		if(Input.GetKeyUp(KeyCode.D)){
			anim.SetBool("Dash",false);
			dashing = false;
		}
		if(Input.GetKeyUp(KeyCode.A)){
			anim.SetBool("Dash",false);
			dashing = false;
		}
		if (dashing && Input.GetKeyDown(KeyCode.J) &&!inHitStun){
			anim.SetTrigger ("DashAttack");
			StartCoroutine (dashAttack ());
		}
		if (!grounded && Input.GetKeyDown (KeyCode.J) &&!inHitStun) {
			anim.SetTrigger ("Jump Attack");
			StartCoroutine (Attack ());
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.U)) {
			anim.SetTrigger ("Punch");
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.K)) {
			anim.SetTrigger ("Launcher");
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.L)) {
			anim.SetTrigger ("Stab");
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.S)) {
			crouching = true;
			anim.SetBool("Crouch",true);
		}
		if (grounded && !inHitStun && Input.GetKeyUp  (KeyCode.S)) {
			anim.SetBool("Crouch",false);
		}
		if (grounded && !inHitStun && Input.GetKeyUp (KeyCode.O)) {
			anim.SetTrigger ("Shoot");
			fire ();
		}

		StartCoroutine (doubleDashRight ());
		StartCoroutine (doubleDashLeft ());
		StartCoroutine (backDash ());
		StartCoroutine (comboAttack());

	}
	void fire(){
		Instantiate (bullet, playerLocation);
	}
	IEnumerator comboAttack(){ 
		if (Input.GetKeyDown (KeyCode.I) && !inComboAttack && grounded) {
			inComboAttack = true;
			bool inAttack2 = false; 
			float delta = 0;
			anim.SetTrigger ("Bash");
			attack2.enabled = true;
			while (delta < 1.2f) {
				delta += Time.deltaTime;
				if (delta > 0.2f) {
					attack2.enabled = false;
				}
				if (Input.GetKeyDown (KeyCode.I) && delta > 0.4f && !inAttack2 && delta < 0.6f) {
					attack2.enabled = false;
					attack3.enabled = true;
					inAttack2 = true; 
					anim.SetTrigger ("Attack");
				}
				if (delta > 0.6f) {
					attack3.enabled = false;
				}
				if (Input.GetKeyDown (KeyCode.I) && delta > 0.9f && inAttack2) {
					inAttack2 = false;
					attack3.enabled = false;
					attack4.enabled = true;
					anim.SetTrigger("Launcher");
				}
				yield return null;
			}
			attack4.enabled = false;
			attack3.enabled = false;
			attack2.enabled = false;
			inAttack2 = false;
			inComboAttack = false;
		}
		if (Input.GetKeyUp (KeyCode.I) && !inComboAttack && !grounded) {
			anim.SetTrigger ("Jump Attack");
			jumpAttack.enabled = true; 
		}
		jumpAttack.enabled = false; 
		yield return null;
	}


	IEnumerator dashAttack(){
		rb2d.AddForce (new Vector2 (0, 0));
		yield return new WaitForSeconds (2);
	
	}
	IEnumerator backDash(){
		if (Input.GetButtonDown("Backdash")&&!inHitStun){
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

			
	IEnumerator boost()
	{
		while (boostMeter < 100) {
			boostMeter += 10;
			yield return new WaitForSeconds (1);
		}
	} 

    IEnumerator Attack()
    {
        attack.enabled = true;
        yield return new WaitForSeconds(0.5f);
        attack.enabled = false;
		attacking = false;
    }
		
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("eAttack")){
			anim.SetTrigger("Hurt");
			GameObject thePlayer = GameObject.Find ("GE");
			EnemyController enemycontroller = thePlayer.GetComponent<EnemyController> ();
			StartCoroutine (waitForHitstun());
			float d = getDirection (enemycontroller.facingRight);
			rb2d.AddForce (new Vector2 (rb2d.velocity.x + d*hitForce, rb2d.velocity.y + hitForce));
			healthSlider.value -= 10;
			if (healthSlider.value <= 0) {
				StartCoroutine (death (other));
			}
		}
		if (other.gameObject.CompareTag("Spike")){
			Application.LoadLevel ("tutorialLevel");
		}
		if (other.gameObject.CompareTag ("Flag")) {
			level = level + 1;
			Application.LoadLevel (level);
		}


	}
	IEnumerator death(Collider2D other ){
		anim.SetTrigger ("Death");
		yield return new WaitForSeconds (1);
		//rb2d.gameObject.SetActive (false);
	}
	IEnumerator waitForHitstun(){
		inHitStun = true;
		yield return new WaitForSeconds (0.5f);
		inHitStun = false;
	}
	IEnumerator doubleDashRight(){
		if (Input.GetKeyUp (KeyCode.D) && facingRight && grounded) {
			float delta = 0;
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				//canDash  = true;
				if (Input.GetKeyDown (KeyCode.D)) {
					dashing = true;
					anim.SetBool ("Dash",true);
					rb2d.AddForce (new Vector2 (moveForce*2, 0));
					if(Input.GetKeyUp(KeyCode.D)){
						dashing = false;
					}
				
				}
				yield return null;
			}
			//canDash = false;
		}
		yield return null;
	}
	IEnumerator doubleDashLeft(){
		if (Input.GetKeyUp (KeyCode.A) && !facingRight && grounded) {
			float delta = 0;
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				//canDash  = true;
				if (Input.GetKeyDown (KeyCode.A)) {
					dashing = true;
					anim.SetTrigger ("Dash");
					rb2d.AddForce (new Vector2 (-moveForce*2, 0));
					if(Input.GetKeyUp(KeyCode.D)){
						dashing = false;
					}

				}
				yield return null;
			}
			//canDash = false;
		}
		yield return null;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.CompareTag("Enemy")) {
			anim.SetTrigger ("Hurt");
			healthSlider.value -= 10;
			StartCoroutine (waitForHitstun());
		}
	}

	bool dash(float timeLimit, float delta, bool buttonPress){
		if (timeLimit < delta && buttonPress) {
			return true;
		}
		return false;
	}

	bool checkIfDoubleAttack(float timeLimit, float delta, bool buttonPress, bool firstAttack){
		if (timeLimit < delta && buttonPress && firstAttack) { return true; }
		return false;
		}

	bool isCombo (int hitstun, int hitTime){
		if (hitstun < hitTime) {
			return false;
		}
		return true; 
	}

	bool canTripleAtk(float timeLimit, float delta, bool buttonPress, bool secondAttack){
		if (delta < timeLimit && buttonPress && secondAttack){
			return true;
	}
		return false;
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
}