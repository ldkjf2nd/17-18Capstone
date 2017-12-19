using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public Rigidbody2D rb2d; 
	public PlayerController playercontroller;
	public float moveForce = 5f; 
	public int EnemyCount;


	// Use this for initialization
	void Start () {

		
		
	}
	
	// Update is called once per frame
	void Update () {
		//enemyTransform.localScale = new Vector3(0f, 0.37f,0f);

		
	}
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		EnemyMoveLeft ();
	}
	void FixedUpdate(){
		
	}


	void EnemyMoveLeft(){
		StartCoroutine (AddLeftForce ());

	}
	IEnumerator AddLeftForce(){
		while(true){
			rb2d.AddForce (Vector2.left * moveForce);
			yield return new WaitForSeconds (1);
		}
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Player") ) {
			if (PlayerController.grounded) {
				Application.LoadLevel(Application.loadedLevel);
			}
			coll.gameObject.GetComponent <Rigidbody2D> ().AddForce (new Vector2 (0f, PlayerController.jumpForce/2));
			rb2d.transform.localScale -= new Vector3(0f, 1.85f, 0f);
			rb2d.gameObject.SetActive (false);
		}  
	}
}
