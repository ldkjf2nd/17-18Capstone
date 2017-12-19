using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip playerHitSound, jumpSound, enemyDeathSound, rangeAttackSound, attackSound, walkSound;
	static AudioSource audioSrc;
	public AudioClip otherClip;

	// Use this for initialization
	void Start () {
		playerHitSound = Resources.Load<AudioClip> ("playerHitSound");
		jumpSound = Resources.Load<AudioClip> ("jumpSound");
		enemyDeathSound = Resources.Load<AudioClip> ("enemyDeathSound");
		rangeAttackSound = Resources.Load<AudioClip> ("RangeAttackSound");
		attackSound = Resources.Load<AudioClip> ("AttackSound");
		walkSound = Resources.Load<AudioClip> ("walkSound");

		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSrc.isPlaying)
		{
			audioSrc.clip = otherClip;
			audioSrc.Play();
		}
		
	}

	public static void PlaySound(string clip)
	{
		switch (clip) {
		case "playerHit":
			audioSrc.PlayOneShot (playerHitSound);
			break;

		case "jump":
			audioSrc.PlayOneShot (jumpSound);
			break;

		case "enemyDeath":
			audioSrc.PlayOneShot (enemyDeathSound);
			break;

		case "rangeAttack":
			audioSrc.PlayOneShot (rangeAttackSound);
			break;

		case "attack":
			audioSrc.PlayOneShot (attackSound);
			break;

		case "walk":
			audioSrc.PlayOneShot (walkSound);
			break;
		}
	}
}
