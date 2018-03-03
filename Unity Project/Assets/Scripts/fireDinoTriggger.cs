using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireDinoTriggger : MonoBehaviour
{
	public GameObject fireDino;
	public Vector3 spawnLocation;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			//gameObject.SetActive (true);
			Instantiate (fireDino, spawnLocation, Quaternion.identity);
			Destroy (this);

		}
	}

}
