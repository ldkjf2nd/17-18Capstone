using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public static int level;
	public static int lives;
	public Text rKitsText; 
	public static int rKits;
	public Text scrapText;
	public static int scrap;
	public static Vector3 respawnPoint;
	public static bool playerControls; 
	void Awake(){
		FindObjectOfType<SoundManagerScript> ().PlayBGM ("level1");
		rKits = 0;
		scrap = 0;
		rKitsText.text = rKits.ToString();
		scrapText.text = scrap.ToString ();
		startPlayerControls ();
		FindObjectOfType<GameManager> ().setRespawn (GameManager.respawnPoint);
		FindObjectOfType<GameManager> ().loadRespawn ();
	}
	void Update(){
		scrapText.text = scrap.ToString();
		rKitsText.text = rKits.ToString();
	}
	public void loadRespawn(){
		GameObject thePlayer2 = GameObject.Find ("GW");
		PlayerController playercontroller = thePlayer2.GetComponent<PlayerController> ();
		playercontroller.rb2d.position = respawnPoint;
	}
	public void stopPlayerControls(){
		playerControls = false; 
	}

	public void startPlayerControls(){
		playerControls = true; 
	}

	public void setRespawn(Vector3 point){
		respawnPoint = point;
	}
	public void playDeath(){
		Application.LoadLevel(level);
	}
	public void nextLevel(){
		level += 1;
		Application.LoadLevel(level);
	}
	public void loadGameOver(){
		
	}
	public void levelReset(){
		level = 0;
	}
	public void resetRespawn(){
		respawnPoint = new Vector3 (0, 0, 0);
	}
	public void increaseScrap(int scrapAmount){
		scrap += 53;
		scrapText.text = rKits.ToString();
	}
	public void increaseRkits(){
		rKits += 1; 
		rKitsText.text = rKits.ToString();
	}

}
