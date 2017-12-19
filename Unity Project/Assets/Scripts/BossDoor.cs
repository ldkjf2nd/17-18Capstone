using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {
	public Rigidbody2D bossDoor;
	public bool isOpen;
	// Use this for initialization
	void Start () {
		bossDoor = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isOpen) {
			bossDoor.MovePosition (transform.position + new Vector3 (0,1,0) * 5*Time.deltaTime);
		}

	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.CompareTag("Player")) {
			isOpen = true;
		}
	}
}
