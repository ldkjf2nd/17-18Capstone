using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Attack {
	public int attackDamage;
	public BoxCollider2D attackCollider; 
	public float startDelayTime; 
	public float durationTime;
	public Vector2 knockBackForce;
	public float hitStunTime;
	public string animationTrigger;
	public string attackSound;
	public float cancelTime;
	public float endTime;
}
