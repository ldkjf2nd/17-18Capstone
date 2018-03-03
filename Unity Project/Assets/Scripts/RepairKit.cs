using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : MonoBehaviour
{
	public Rigidbody2D rb2d;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			FindObjectOfType<SoundManagerScript> ().PlaySound ("itemCollection");
			rb2d.gameObject.SetActive (false);
		}

	}
}