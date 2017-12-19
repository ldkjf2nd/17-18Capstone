using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
	private Rigidbody2D rb2d;
	public float delta;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();

		delta = Mathf.Abs (playercontroller2.rb2d.position.x - rb2d.position.x);
		if (delta < 1) {
			rb2d.simulated = true;
		}
	}
}
