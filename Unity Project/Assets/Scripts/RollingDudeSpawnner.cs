using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDudeSpawnner : MonoBehaviour
{
	public Rigidbody2D rb2d;
	public Transform rollingDude;
	public float delta;
	public float deltaY;
	private float count;
	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();
		delta = Mathf.Abs (playercontroller2.rb2d.position.x - rb2d.position.x);
		deltaY = Mathf.Abs (playercontroller2.rb2d.position.y - rb2d.position.y);
		if (delta < 10 && deltaY < 5 && count < 2) {
			count++;
			StartCoroutine (eSpawn ());	
		}
	}

	IEnumerator eSpawn ()
	{
		Instantiate (rollingDude, rb2d.transform);
		yield return new WaitForSeconds (2f);
		Instantiate (rollingDude, rb2d.transform);
	

	}
}
