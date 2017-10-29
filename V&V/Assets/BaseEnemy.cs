using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
	public float enemyHealth;
	public object equippedWeapon;
	public float chanceToDropScrap;
	public bool doesDropScrap;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void EnemyTakesDamage(float damageAmount) 
	{
		enemyHealth -= damageAmount;
	}

	public void dropScrap()
	{

	}
}