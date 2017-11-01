using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy: MonoBehaviour {
	public float enemyHealth;
	public float armorRating = 50f;
	public GameObject equippedWeapon;
	public float chanceToDropScrap;
	public bool doesDropScrap;

	void Start () {
		
	}
		
	void Update () {
		
	}

	public void EnemyTakesDamage(float damageAmount) 
	{
		enemyHealth -= calculateHealthLossAfterArmor(damageAmount);
	}

	public float calculateHealthLossAfterArmor(float damageAmount)
	{
		float returable;
		returable = damageAmount * (1f - (armorRating / 100f));

		return returable;
	}

	public void findPlayerOnScreen()
	{
	}

	public void useBasicAttack()
	{
	}

	public void useSpecialAttack()
	{
	}

	public void showSpawnAnimation() 
	{
	}

	public void showDeathAnimation()
	{
	}

	public void dropScrap()
	{

	}
}