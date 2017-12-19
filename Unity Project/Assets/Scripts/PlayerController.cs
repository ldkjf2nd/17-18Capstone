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
	public bool attacking = false;
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
	public Vector3 startLocation;
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
	public bool isDead;
	public Transform explosion; 
	public Slider energySlider;
	public GameObject dashFlame;
	public Text rKitsText; 
	public int rKits;
	public Text scrapText;
	public static int scrap; 
	public SpriteRenderer sp2d;
	public BoxCollider2D bc2d;
	private Vector2 pvSize = new Vector2 (0.33f, 0.96f);
	public GameObject shopMenu;
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
		rKits = 0;
		scrap = 0;
		rKitsText.text = rKits.ToString();
		scrapText.text = scrap.ToString ();
		bc2d.size = pvSize;
		BossTrigger.bossIntro = false; 
		BossTrigger.bossStart = false;
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
		SoundManagerScript.PlaySound("jump");
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
		scrapText.text = scrap.ToString();
		rKitsText.text = rKits.ToString();

		if (grounded && !inHitStun) {
			anim.SetTrigger ("Grounded"); 
		}
		if (!grounded && !attacking && !inHitStun ) {
			anim.SetTrigger ("Jump");
		}
		if (h > 0 &&!inHitStun && !isDead && !dashing &&!crouching) {
			if (!facingRight) {
				flip ();
			}
			playerMoveRight ();
		}

		if (h < 0 &&!inHitStun && !isDead && !dashing &&!crouching) {
			if (facingRight) {
				flip ();
			}
			playerMoveLeft ();
		}

		if (h == 0 &&!inHitStun && !isDead) {
			anim.SetTrigger ("Idle");
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}

		if (Input.GetButtonDown ("Jump") && grounded &&!inHitStun && !isDead) {
			playerJump ();
		}

		/* if (Input.GetButton ("Jump") && !grounded &&!inHitStun) {
			if (boostMeter > 0) {
				boostMeter -= 10;
				rb2d.AddForce (new Vector2 (rb2d.velocity.x, 20f));
			}
		} */

		if (Input.GetKeyDown (KeyCode.J) &&!inHitStun && grounded && !dashing && !attacking) {
			attacking = true;
			anim.SetTrigger ("Attack");
			SoundManagerScript.PlaySound("attack");
			StartCoroutine (Attack ());
		}
		if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) ){
			anim.SetBool("Dash",false);
			dashing = false;
			dashFlame.SetActive(false);
		}
		if (dashing && Input.GetKeyDown(KeyCode.J) &&!inHitStun && !attacking && !isDead && grounded){
			anim.SetTrigger ("DashAttack");
			dashing = false;
			float d = getDirection (facingRight);
			rb2d.velocity = new Vector2 (5*d, 0);
			StartCoroutine (Attack ());

		}
		if (!grounded && Input.GetKeyDown (KeyCode.J) &&!inHitStun && !attacking && !isDead && !dashing) {
			attacking = true;
			anim.SetTrigger ("Jump Attack");
			SoundManagerScript.PlaySound("attack");
			StartCoroutine (Attack ());
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.U)) {
			anim.SetTrigger ("Punch");
			SoundManagerScript.PlaySound("attack");
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.K)) {
			anim.SetTrigger ("Launcher");
			SoundManagerScript.PlaySound("attack");
		}
		if (grounded && !inHitStun && Input.GetKeyDown (KeyCode.L)) {
			anim.SetTrigger ("Stab");
			SoundManagerScript.PlaySound("attack");
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
			SoundManagerScript.PlaySound("rangeAttack");
			fire ();
		}
		if (healthSlider.value <= 0 && !isDead) {
			StartCoroutine (death());
		}
		if (Input.GetKeyDown (KeyCode.Q)&& rKits > 0) {
			rKits -= 1;
			healthSlider.value = 100; 
			rKitsText.text = rKits.ToString();
		}

		StartCoroutine (doubleDashRight ());
		StartCoroutine (doubleDashLeft ());
		StartCoroutine (backDash ());
		StartCoroutine (comboAttack());

	}
	void fire(){
		energySlider.value -= 1;
		Instantiate (bullet, playerLocation);
	}
	IEnumerator comboAttack(){ 
		if (Input.GetKeyDown (KeyCode.I) && !inComboAttack && grounded &&!inHitStun && !attacking && !isDead) {
			inComboAttack = true;
			bool inAttack2 = false; 
			float delta = 0;
			anim.SetTrigger ("Bash");
			SoundManagerScript.PlaySound("attack");
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
					SoundManagerScript.PlaySound("attack");
				}
				if (delta > 0.6f) {
					attack3.enabled = false;
				}
				if (Input.GetKeyDown (KeyCode.I) && delta > 0.9f && inAttack2) {
					inAttack2 = false;
					attack3.enabled = false;
					attack4.enabled = true;
					anim.SetTrigger("Launcher");
					SoundManagerScript.PlaySound("attack");
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
			SoundManagerScript.PlaySound("attack");
			jumpAttack.enabled = true; 
		}
		jumpAttack.enabled = false; 
		yield return null;
	}

	IEnumerator backDash(){
		if (Input.GetButtonDown("Backdash")&&!inHitStun &&!attacking &&!isDead ){
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
			SoundManagerScript.PlaySound("playerHit");
			GameObject thePlayer = GameObject.Find ("GE");
			EnemyController enemycontroller = thePlayer.GetComponent<EnemyController> ();
			StartCoroutine (blink ());
			StartCoroutine (waitForHitstun());
			float d = getDirection (enemycontroller.facingRight);
			rb2d.AddForce (new Vector2 (rb2d.velocity.x + d*hitForce, rb2d.velocity.y + hitForce));
			healthSlider.value -= 10;
		}
		if (other.gameObject.CompareTag("Spike")){
			healthSlider.value -= 100;
			//StartCoroutine (death(other));
		}
		if (other.gameObject.CompareTag ("Flag")) {
			level = level + 1;
			Application.LoadLevel (level);
		}
		if (other.gameObject.CompareTag ("Shop")) {
			shopMenu.SetActive (true);
		}

	}
	IEnumerator death(){
		isDead = true;
		anim.SetTrigger ("Hurt");
		Instantiate (explosion, playerLocation);
		SoundManagerScript.PlaySound("playerHit");
		yield return new WaitForSeconds (1);
		Application.LoadLevel (level);
		//rb2d.gameObject.SetActive (false);
	}
	IEnumerator waitForHitstun(){
		inHitStun = true;
		yield return new WaitForSeconds (0.5f);
		inHitStun = false;
	}
	IEnumerator doubleDashRight(){
		if (Input.GetKeyUp (KeyCode.D) && facingRight && grounded &&!inHitStun &&!attacking &&!isDead &&!crouching) {
			float delta = 0;
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				//canDash  = true;
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
			//canDash = false;
		}
		yield return null;
	}
	IEnumerator doubleDashLeft(){
		if (Input.GetKeyUp (KeyCode.A) && !facingRight && grounded &&!inHitStun &&!attacking &&!isDead &&!crouching) {
			float delta = 0;
			while (delta < 0.2f) {
				delta += Time.deltaTime;
				//canDash  = true;
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
			//canDash = false;
		}
		yield return null;
	}
	IEnumerator blink(){
		sp2d.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		sp2d.color = Color.white;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.CompareTag("Enemy")) {
			rb2d.velocity = (new Vector2 (0, 0));
			float d = getDirection (facingRight);
			rb2d.AddForce (new Vector2(300f*-d, 300f));
			healthSlider.value -= 1;
			anim.SetTrigger ("Hurt");
			StartCoroutine (blink ());
		}
		if (other.collider.CompareTag ("Fire Dino")) {
			healthSlider.value -= 10;
			anim.SetTrigger ("Hurt");
			StartCoroutine (blink ());
		}
		if (other.collider.CompareTag ("RepairKit")) {
			rKits += 1; 
			rKitsText.text = rKits.ToString();
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
}