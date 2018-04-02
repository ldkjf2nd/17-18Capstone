using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firePillar : MonoBehaviour {
	private BoxCollider2D hitBox;
	public Vector2 offset;
	//public float startTime;
	public float duration;
	public float waitTime;
	public bool inFlame;
	public bool inWait;
	private Transform transform;
	private SpriteRenderer sprite;
	private float time; 
	private Vector2 org;
	private Vector3 post;
	private Animator anim;
	// Use this for initialization
	void Start () {
		hitBox = GetComponent<BoxCollider2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		transform = GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
		org = hitBox.offset;
		post = transform.position;
		time = 0;
		transform.position = new Vector3 (transform.position.x, transform.position.y - 2.75f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		time +=Time.deltaTime;

		if (time < waitTime) {
			StartCoroutine (wait());
		}
			
		if (time >waitTime  && !inFlame) {
			StartCoroutine (display (offset,duration));
		}
		if (time > waitTime + duration) {
			time = 0;
		}


	}
	public IEnumerator display(Vector2 offset, float duration){
		inFlame = true;
		anim.SetBool ("inFlame",true);
		transform.position = post;
		hitBox.offset = offset;
		yield return new WaitForSeconds(duration);
		hitBox.offset = org;
		time = 0;
		transform.position = new Vector3 (transform.position.x, transform.position.y - 2.75f, transform.position.z);
		anim.SetBool ("inFlame",false);
		inFlame = false;
	}
	public IEnumerator wait(){
		inWait = true;
		yield return new WaitForSeconds(duration);
		inWait= false;
	}
		
}
