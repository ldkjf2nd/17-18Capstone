using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
	public void buyItem(Item item){
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller = thePlayer2.GetComponent<PlayerController> ();
		if (GameManager.scrap - item.cost < 0) {
			FindObjectOfType<SoundManagerScript> ().PlaySound ("error");
		} else {
			GameManager.scrap = GameManager.scrap - item.cost;
			FindObjectOfType<SoundManagerScript> ().PlaySound ("itemCollection");
			item.model.SetActive(false);
			item.costText.enabled =  false;
			if (item.type == "def") {
				FindObjectOfType<GameManager>().increaseHealth(item.healthBoost);
			}
			if (item.type == "r") {
				GameManager.rKits += 1;
			}
			if (item.type == "dmg") {
				FindObjectOfType < GameManager> ().increaseDamage (item.cost);
			}
		}
	}
}
