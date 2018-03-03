using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public GameObject shopMenu;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void onTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Attack")) {
			shopMenu.SetActive (true);
		}

	}


}
