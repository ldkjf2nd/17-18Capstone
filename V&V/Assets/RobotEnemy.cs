using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemy : BaseEnemy {
	public GameObject bullet;
	Vector2 bulletPos;
	public float fireRate = 0.5f;
	public float nextFire = 0.0f;

	void Start () {
		
	}
	
	void Update () {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			fire ();
		}
	}

	void fire() {
		bulletPos = transform.position;
		bulletPos += new Vector2 (1.27f, 0f);
		Instantiate (bullet, bulletPos, Quaternion.identity);
	}
}
