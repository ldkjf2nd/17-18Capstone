using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
	public void buyItem(Item item){
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller = thePlayer2.GetComponent<PlayerController> ();

		if (PlayerController.scrap - item.cost < 0) {
			print ("no money");
		} else {
			PlayerController.scrap = PlayerController.scrap - item.cost;
			item.model.SetActive(false);
			item.costText.enabled =  false;
			if (item.type == "def") {
				PlayerController.def += 1;
			}
			if (item.type == "r") {
				playercontroller.rKits += 1;
			}
			if (item.type == "dmg") {
				PlayerController.dmgUp += 1;
			}
		}
	}
}
