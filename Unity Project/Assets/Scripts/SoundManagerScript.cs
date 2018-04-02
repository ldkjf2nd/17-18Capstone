using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
	public AudioClip[] sounds;
	public AudioClip[] bgm; 
	public string[] loopList;
	public string[] bossList;
	public AudioSource soundSource;
	public AudioSource bgmSource;


	public bool stopMusic;

	void Awake(){
		soundSource = GetComponent<AudioSource>();
		bgmSource= GetComponent<AudioSource>();
	}
		
	void Update()
	{
		if (!bgmSource.isPlaying && !stopMusic) {
			PlayBGM (loopList[GameManager.level]);
		}
	}

	//method for choosing the respective sound effect for the action.
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

		case "attack1":
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

		case "attack2":
			soundSource.PlayOneShot (sounds[20]);
			break;
		
		case "attack3":
			soundSource.PlayOneShot (sounds[21]);
			break;
	}

	}

	//method for choosing the respective BGM of a level
	public void PlayBGM(string clip)
	{
		switch (clip) {
		case "level1":
			bgmSource.PlayOneShot (bgm[0]);
			break;

		case "level1Boss":
			bgmSource.PlayOneShot (bgm[1]);
			break;
		
		case "level2":
			bgmSource.PlayOneShot (bgm[2]);
			break;

		case "level2Boss":
			bgmSource.PlayOneShot (bgm[3]);
			break;
		
		case "level3":
			bgmSource.PlayOneShot (bgm[4]);
			break;

		case "level3Boss":
			bgmSource.PlayOneShot (bgm[5]);
			break;

		case "level4":
			bgmSource.PlayOneShot (bgm[6]);
			break;

		case "level4Boss":
			bgmSource.PlayOneShot (bgm[7]);
			break;

		case "level5":
			bgmSource.PlayOneShot (bgm[8]);
			break;

		case "level5Boss":
			bgmSource.PlayOneShot (bgm[9]);
			break;

		case "main":
			bgmSource.PlayOneShot (bgm[9]);
			break;
		}
	}

	//method for stop playback
	public void stopBGM(){
		stopMusic = true;
		bgmSource.Stop ();
	}

	//method for start playback
	public void startBGM(){
		stopMusic = false;
	}

}