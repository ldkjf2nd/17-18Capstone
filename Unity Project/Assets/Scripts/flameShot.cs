using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameShot : MonoBehaviour
{
	//public Rigidbody2D player;
	private Rigidbody2D rb2d;
	private Transform tr2d;
	public float delta = 0;
	public float direction;
	public Vector2 shotVelocity = new Vector2 (1, 0);
	public float startTime;
	public float currentTime;
	private GameObject bullet;
	public Transform bulletHolder;
	// Use this for initialization
	void Start ()
	{
		tr2d = GetComponent<Transform> ();	
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.simulated = true;
		rb2d.gravityScale = 0;
		GameObject thePlayer2 = GameObject.Find ("fireDino");
		fireDinoController fireDino = thePlayer2.GetComponent<fireDinoController> ();
		if (fireDino.facingRight) {
			rb2d.velocity = new Vector2 (10f, 0f);
		} else {
			rb2d.velocity = new Vector2 (-10f, 0f);
		}
		this.gameObject.transform.SetParent (bulletHolder);
	}

	// Update is called once per frame
	void Update ()
	{
		Destroy (this.gameObject, 4);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player") || other.CompareTag ("Wall")) {
			Destroy (this.gameObject);
		}
	}
}
