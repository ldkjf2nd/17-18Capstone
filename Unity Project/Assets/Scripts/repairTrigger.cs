using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repairTrigger : MonoBehaviour
{
	public Item repairKit;
	public TextMesh cost;

	void Start ()
	{
		cost.text = repairKit.cost.ToString ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			FindObjectOfType<ShopManager> ().buyItem (repairKit);
		}
	}
}
