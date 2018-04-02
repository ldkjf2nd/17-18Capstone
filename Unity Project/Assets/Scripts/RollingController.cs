using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingController : MonoBehaviour {
	public Rigidbody2D rb2d;
	public bool onGround = false;
	public bool facingRight = true;
	private float horizontalForce = 600f; 
	private float jumpForce = 1200f;
	public Transform groundCheck;
	public Transform explosion;
	public float delta;
	public int health;
	public float deltaY;
	private CircleCollider2D hitbox;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		hitbox = GetComponent<CircleCollider2D> ();
		facingRight = true;
		onGround = false;
		horizontalForce = 300f; 
		jumpForce = 300f;
		health = 10;
	}
	
	// Update is called once per frame
	void Update () {
		// find position of player character to determine action
		onGround = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();
		delta = Mathf.Abs (playercontroller2.rb2d.position.x - rb2d.position.x);
		deltaY = Mathf.Abs (playercontroller2.rb2d.position.y - rb2d.position.y);

		// change orientation of self depending on position of player
		if ((playercontroller2.rb2d.position.x > rb2d.position.x) && onGround && delta < 10 && deltaY < 5 ) {
			if (!facingRight){
				flip ();
			}
			//
			JumpAttack (1f);
		}
		if (playercontroller2.rb2d.position.x < rb2d.position.x && onGround && delta < 10 && deltaY < 5 ) {
			if (facingRight) {
				flip ();
			}
			JumpAttack (-1f);
		}
	}

	//method for attack(lunge)
	public void JumpAttack(float direction){
		rb2d.velocity = (new Vector2 (0, 0));
		rb2d.AddForce(new Vector2(horizontalForce*direction, jumpForce));
	}

	// method for flipping the orientation
	void flip (){
		facingRight = !facingRight; 
		Vector3 theScale = transform.localScale; //teScale is temporary variable
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// collision detection, health counter, and death trigger.
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Attack")) {
			FindObjectOfType<GameManager> ().increaseEnergy (10);
			health -= 25;
			if (health <= 0) {
				FindObjectOfType<GameManager> ().increaseScrap (12);
				StartCoroutine (destroy ());
			}
		}
		if (coll.gameObject.CompareTag ("Player")) {
			Slider[] hs = GameObject.Find ("HS").GetComponents<Slider> ();
			FindObjectOfType<GameManager> ().removeHealth (hs,1);
		}



	}

	// death initiate animation and destroy object.
	IEnumerator destroy(){
		Instantiate (explosion, rb2d.transform);
		hitbox.enabled = false;
		FindObjectOfType<SoundManagerScript> ().PlaySound ("enemyDeath");
		yield  return new WaitForSeconds(0.5f);
		rb2d.gameObject.SetActive (false);
	}
}
