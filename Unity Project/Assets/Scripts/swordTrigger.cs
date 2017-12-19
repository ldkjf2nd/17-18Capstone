using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordTrigger : MonoBehaviour {
	public Item sword; 
	public TextMesh cost; 
	void Start(){
		cost.text = sword.cost.ToString();
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			FindObjectOfType<ShopManager> ().buyItem (sword);
		}
	}

}
