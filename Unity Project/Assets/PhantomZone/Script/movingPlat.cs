using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlat : MonoBehaviour {
	private Rigidbody2D rb2d;
	public bool inAction;
	public float distance; 
	public float speed;
	private Vector3 startPos;
	public float startTime;
	private float time = 0; 
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > startTime) {
			Vector3 org = startPos; 
			org.x += distance * Mathf.Sin (Time.time * speed);
			transform.position = org;
		}
	}

}
