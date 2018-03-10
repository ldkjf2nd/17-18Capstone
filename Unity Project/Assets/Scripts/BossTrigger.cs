using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
	public Rigidbody2D bossDoor;
	public Rigidbody2D player;
	public Vector3 position; 
	public bool isOpen;
	public static bool bossIntro = false;
	public static bool bossStart = false; 
	public Dialogue dialogue;
	// Use this for initialization
	void Start () {
		bossDoor = GetComponent<Rigidbody2D>();
		isOpen = false;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isOpen) {
			bossDoor.MovePosition (transform.position + new Vector3 (0, 1, 0) * 5 * Time.deltaTime);
			player.position = Vector3.MoveTowards (player.position, position, 10 * Time.deltaTime);
			FindObjectOfType<GameManager> ().stopPlayerControls ();
			if (player.position.x >= position.x-0.5 && player.position.y >= position.y) {
				isOpen = false;
				bossIntro = true;
				TriggerDialogue();

			}

		}
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			FindObjectOfType<SoundManagerScript > ().PlaySound ("openDoor");
			FindObjectOfType<SoundManagerScript>().stopMusic ();
			isOpen = true;
			FindObjectOfType<GameManager> ().setRespawn (new Vector3(191.4f, 32.9f, -3f));
		}
	}
	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}
}
