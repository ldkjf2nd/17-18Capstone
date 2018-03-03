using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
	public Transform hero;
	public Transform camera;
	private Vector3 offset;
	public Vector3 bossPosition;


	// Use this for initialization
	void Start ()
	{
		camera = GetComponent<Transform> ();
		camera.position = hero.position;
		offset = new Vector3 (0, 2, -10);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (!BossTrigger.bossIntro) {
			transform.position = hero.transform.position + offset;
		}
		if (BossTrigger.bossIntro) {
			transform.position = Vector3.MoveTowards (transform.position, bossPosition, 10 * Time.deltaTime);
			if (transform.position.x == bossPosition.x && transform.position.y == bossPosition.y) {
				DialogueManager.inDialogue = true;
			}
		}
	}

}
