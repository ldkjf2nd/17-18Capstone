using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
	public AudioClip[] sounds;
	public AudioClip[] bgm; 
	public AudioSource soundSource;
	public AudioSource bgmSource;
	public void PlaySound(string clip)
	{
		switch (clip) {
		case "playerHit":
			soundSource.PlayOneShot (sounds[0]);
			break;

		case "jump":
			soundSource.PlayOneShot (sounds[1]);
			break;

		case "enemyDeath":
			soundSource.PlayOneShot (sounds[2]);
			break;

		case "rangeAttack":
			soundSource.PlayOneShot (sounds[3]);
			break;

		case "attack":
			soundSource.PlayOneShot (sounds[4]);
			break;

		case "walk":
			soundSource.PlayOneShot (sounds[5]);
			break;
		case "bossLand":
			soundSource.PlayOneShot (sounds [6]);
			break;
		case "dash":
			soundSource.PlayOneShot (sounds [7]);
			break;
		case "explosion":
			soundSource.PlayOneShot (sounds [8]);
			break;
		case "fireShot":
			soundSource.PlayOneShot (sounds [9]);
			break;
		case "itemCollection":
			soundSource.PlayOneShot (sounds [10]);
		break;
		case "takeDamage":
			soundSource.PlayOneShot (sounds [11]);
		break;
		case "error":
		soundSource.PlayOneShot (sounds [12]);
		break;
		case "openDoor":
		soundSource.PlayOneShot (sounds [13]);
		break;
		case "nextDialogue":
		soundSource.PlayOneShot (sounds [14]);
		break;
		case "repair":
		soundSource.PlayOneShot (sounds [15]);
		break;
	}

	}
	public void PlayBGM(string clip)
	{
		switch (clip) {
		case "level1":
			bgmSource.PlayOneShot (bgm[0]);
			break;

		case "level1Boss":
			bgmSource.PlayOneShot (bgm[1]);
			break;

		}
	}
	public void stopMusic(){
		bgmSource.Stop ();
	}
}