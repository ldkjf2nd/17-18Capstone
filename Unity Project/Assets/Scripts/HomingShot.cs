using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShot : MonoBehaviour {
	//public Rigidbody2D player;
	private Rigidbody2D rb2d;
	private Transform tr2d;
	public float delta = 0;
	public float deltaY;
	public float direction;
	public Vector2 shotVelocity = new Vector2 (1, 0);
	public float startTime;
	public float currentTime;
	private GameObject bullet;
	public Transform bulletHolder;
	public Vector3 lastPosition;
	// Use this for initialization
	void Start () {
		tr2d = GetComponent<Transform> ();	
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.simulated = true;
		rb2d.gravityScale = 0;
		GameObject fDino = GameObject.Find ("fireDino");
		GameObject PC1 = GameObject.Find ("GW");
		PlayerController PC = PC1.GetComponent<PlayerController> ();
		fireDinoController fireDino = fDino.GetComponent<fireDinoController> ();
		lastPosition = PC.rb2d.position;
		this.gameObject.transform.SetParent (bulletHolder);
		//rb2d.MovePosition (player.position);
		//startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		GameObject PC1 = GameObject.Find ("GW");
		PlayerController PC = PC1.GetComponent<PlayerController> ();
		delta = Mathf.Abs (PC.rb2d.position.x - rb2d.position.x);
		deltaY = Mathf.Abs (PC.rb2d.position.y - rb2d.position.y);
		float step = 10 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, lastPosition, step);
		Destroy (this.gameObject, 3);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")|| other.CompareTag("Wall")) {
			Destroy (this.gameObject);
		}
	}
}
