using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopTrigger : MonoBehaviour
{
	public GameObject shopMenu;
	public Rigidbody2D rb2d;
	public float deltaY;
	public float delta;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();
		deltaY = Mathf.Abs (playercontroller2.rb2d.position.y - rb2d.position.y);
		delta = Mathf.Abs (playercontroller2.rb2d.position.x - rb2d.position.x);
		if (delta < 10) {
			shopMenu.SetActive (true);
		} else {
			shopMenu.SetActive (false);
		}

	}
}
