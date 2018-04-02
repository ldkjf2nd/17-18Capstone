using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertical : MonoBehaviour {
	public int ebdmg; 
	public int ebspeed;
	public GameObject bullet;
	private Rigidbody2D rb2d;
	public float speed; 
	public float target;
	public int dmg;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x,target, transform.position.z), step);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Enemy") || other.CompareTag("Fire Dino")) {
			Destroy (this.gameObject);
		}
		if (other.CompareTag ("Wall") ) {
			Destroy (this.gameObject);
			GameObject temp = GameObject.Find ("GW");
			PlayerController gw = temp.GetComponent<PlayerController> ();
			GameObject go =	(GameObject)Instantiate (bullet,transform.position, transform.rotation);
			setBullet (go, ebdmg, gw.transform.position, ebspeed);
		}
	}
	void setBullet(GameObject go, int dmg, Vector3 position, int speed){
		go.GetComponent<elctrobullet>().dmg = dmg;
		go.GetComponent<elctrobullet> ().speed = speed;
		go.GetComponent<elctrobullet> ().target = position;
	}
}
