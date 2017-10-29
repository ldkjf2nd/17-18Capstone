	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController	 : MonoBehaviour {
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false; 

	public float moveForce = 365f; 
	public float maxSpeed = 5f;
	public static float jumpForce = 800f;
	public Transform groundCheck; 

	public static bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d; 

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		jump = false;
		
	}
	
	// Update is called once per frame
	void Update(){
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground")); 
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true; 
		}
	}
	void FixedUpdate(){
		float h = Input.GetAxis("Horizontal");
			anim.SetFloat("Speed", Mathf.Abs(h));
		if (h * rb2d.velocity.x < maxSpeed) {
			rb2d.AddForce (Vector2.right * h * moveForce);
		}
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {// velocity is positive or negative 
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}
		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}
		if (jump){
			PlayerJump ();
		}
		if(Input.GetKeydown("Fire1"){
			
	
	}

	void PlayerJump (){
		anim.SetTrigger("Jump");
		rb2d.AddForce(new Vector2(0f,jumpForce));
		jump = false;	
	}



	void Flip (){
		facingRight = !facingRight; 
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void OnTriggerEnter2D(Collider2D other){
	}
}
