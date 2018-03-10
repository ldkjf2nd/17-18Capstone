using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour {
	public bool isPlayerNear;
	public AttackDetection(){
		this.isPlayerNear = false;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")){
			isPlayerNear = true;
		}
		
	}

}
