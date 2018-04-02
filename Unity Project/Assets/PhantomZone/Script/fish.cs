using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fish : MonoBehaviour {
	private Animator anim;
	public int health;
	private Rigidbody2D rb2d;
	public bool inAction;
	public Transform explosion;
	public bool facingRight;
	public float distance; 
	public float speed;
	public bool inFlip;
	public bool otherInHit;
	private Vector3 startPos;
	private BoxCollider2D hitbox;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		hitbox = GetComponent<BoxCollider2D> ();
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 org = startPos; 
		org.x += distance * Mathf.Sin (Time.time * speed);
		if (Mathf.Sin (Time.time * speed) > 0.9f && !inFlip) {
			StartCoroutine (flipd ());
		}
		if (Mathf.Sin (Time.time * speed) < -0.9f && !inFlip) {
			StartCoroutine (flipd ());
		}
		transform.position = org;


	}
	void OnTriggerEnter2D(Collider2D coll){
		GameObject thePlayer = GameObject.Find ("GW");
		PlayerController player = thePlayer.GetComponent<PlayerController> ();
		if (coll.gameObject.CompareTag ("Attack")) {
			enemyGetHit(player.attacks[0]);
		}
		if (coll.gameObject.CompareTag ("Attack2") || coll.gameObject.CompareTag ("Attack3") ) {
			enemyGetHit(player.attacks[1]);
		}
		if (coll.gameObject.CompareTag ("Launcher")){
			enemyGetHit(player.attacks[2]);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			otherInHit = false; 
		}
	}

	IEnumerator destroy(){
		Instantiate (explosion, rb2d.transform);
		hitbox.enabled = false;
		FindObjectOfType<SoundManagerScript> ().PlaySound ("enemyDeath");
		yield  return new WaitForSeconds(0.5f);
		rb2d.gameObject.SetActive (false);
	}

	public void enemyGetHit(Attack attack){
		anim.SetTrigger ("Damage");
		health -= attack.attackDamage;
		rb2d.AddForce (attack.knockBackForce);

		StopAllCoroutines ();
		if (health <= 0) {
			FindObjectOfType<GameManager> ().increaseScrap (12);
			StartCoroutine (destroy ());
		}
		//calculateKnockback ( hitForce);
	}
	public IEnumerator flipd(){
		inFlip = true;
		flip ();
		yield return new WaitForSeconds (1f);
		inFlip = false;
	}
	void flip (){
		facingRight = !facingRight; 
		Vector3 theScale = transform.localScale; //teScale is temporary variable
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
