using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elctrobullet : MonoBehaviour {
	private Rigidbody2D rb2d;
	public float speed; 
	public Vector3 target;
	public int dmg;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target, step);
		Destroy (this.gameObject, 3);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			FindObjectOfType<PlayerController> ().getHit (0.5f,dmg,new Vector2(0,0));
			Destroy (this.gameObject);
		}	
	}
}
