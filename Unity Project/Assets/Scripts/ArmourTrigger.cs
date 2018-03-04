using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourTrigger : MonoBehaviour
{
	public Item armour;
	public TextMesh cost;

	void Start ()
	{
		cost.text = armour.cost.ToString ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			FindObjectOfType<ShopManager> ().buyItem (armour);
		}
	}
}
