using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Main controller of player character functions, 
// camera, UI canvas, and sound manager
// Holds separate abstract components under one object.
public class GameManager : MonoBehaviour
{
	public static int level;
	public static int lives;
	public static int rKits;

	public static int scrap;
	public static Vector3 respawnPoint;
	public static bool playerControls;
	public Vector3 startLocation;

	public GameObject camera;
	public GameObject player;
	public GameObject playerCanvas;
	public GameObject soundManager;
	public GameObject pauseMenu;
	public GameObject miniMap;
	public GameObject mmCamera;
	public Vector3[] bossPosition;

	public int wepBoost; 
	public int armBoost;


	void Awake ()
	{
		// set up references to prefabs and objects 
		level = SceneManager.GetActiveScene ().buildIndex;
		if (level != 0) {
			setRespawn (respawnPoint);
			GameObject gw = (GameObject)Instantiate (player, respawnPoint, transform.rotation);
			gw.name = "GW";
			gw.transform.position = respawnPoint;
			GameObject cam = (GameObject)Instantiate (camera, startLocation, transform.rotation);
			GameObject pc = (GameObject)Instantiate (playerCanvas, new Vector3(0,0,0), transform.rotation);	
			GameObject sm = (GameObject)Instantiate (soundManager, startLocation, transform.rotation);	
			GameObject pm = (GameObject)Instantiate (pauseMenu, startLocation, transform.rotation);	
			GameObject mm = (GameObject)Instantiate (miniMap, startLocation, transform.rotation);
			GameObject mc = (GameObject)Instantiate (mmCamera, startLocation, transform.rotation);
			sm.name = "SoundManager";
			cam.GetComponent<cameraScript> ().hero = gw.transform;
			cam.GetComponent<cameraScript> ().bossPosition = bossPosition [level];
			mc.GetComponent<cameraScript> ().hero = gw.transform;
			mc.GetComponent<cameraScript> ().bossPosition = bossPosition [level];


			Slider[] hs = GameObject.Find ("HS").GetComponents<Slider> ();
			Slider[] es = GameObject.Find ("ES").GetComponents<Slider> ();
			Text[] st = GameObject.Find ("ST").GetComponents<Text> ();
			Text[] rt = GameObject.Find ("RT").GetComponents<Text> ();

			startPlayerControls ();
			BossTrigger.bossIntro = false; 
			BossTrigger.bossStart = false; 
		}
	}


	void Update ()
	{	
		// maintaining reference to components and game objects
		// listen input of cheats for debugging purposes
		if (level != 0) {
			Text[] st = GameObject.Find ("ST").GetComponents<Text> ();
			Text[] rt = GameObject.Find ("RT").GetComponents<Text> ();
			Slider[] hs = GameObject.Find ("HS").GetComponents<Slider> ();
			st [0].text = scrap.ToString ();
			rt [0].text = rKits.ToString ();
			;

			if (Input.GetButtonDown ("cheat")) {
				nextLevel ();
			}
			if (Input.GetButtonDown ("cheat2")) {
				GameObject.Find ("GW").transform.position = new Vector3 (190f, 40f, 0f);
			}
			if (Input.GetButtonDown ("cheat3")) {
				StartCoroutine (FindObjectOfType<PlayerController> ().death ());
			}
			if (Input.GetButtonDown ("cheat4")) {
				levelReset ();
			}
			if (Input.GetButtonDown ("cheat5")) {
				GameObject.Find ("GW").transform.position = new Vector3 (130f, -25f, 0f);
			}
			if (Input.GetButtonDown ("cheat6")) {
				GameObject.Find ("GW").transform.position = new Vector3 (-88f, -43f, 0f);
			}
		}

	}

	//methods for increasing stats
	public void increaseDamage(int dmg)
	{
		wepBoost+= dmg;
	}
		

	public void increaseHealth(int health)
	{
		Slider[] hs = GameObject.Find ("HS").GetComponents<Slider> ();
		hs[0].maxValue += health;
	}
		
	public void removeHealth (Slider[] hs, int value)
	{
		hs [0].value -= value;
	}

	//methods for setting UI sliders
	public void setHs (Slider[] hs, int value)
	{
		hs [0].value = value;
	}

	public void setEs (Slider[] es, int value)
	{
		es [0].value = value;
	}

	//methods for activate/deactivate player control during cutscenes
	public void stopPlayerControls ()
	{
		playerControls = false; 
	}

	public void startPlayerControls ()
	{
		playerControls = true; 
	}

	// method for respawn and death
	public void setRespawn (Vector3 point)
	{
		respawnPoint = point;
	}

	public void playDeath ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (level);
	}

	// next for level transitions
	public void nextLevel ()
	{
		level += 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene (level);
	}

	public void levelReset ()
	{
		level = 1;
	}


	public void resetRespawn ()
	{
		respawnPoint = new Vector3 (0, 0, 0);
	}

	// Methods for resource/economy changes
	public void increaseEnergy (int energy)
	{
		Slider[] es = GameObject.Find ("ES").GetComponents<Slider> ();
		es [0].value += energy;
	}

	public void decreaseEnergy (int energy)
	{
		Slider[] es = GameObject.Find ("ES").GetComponents<Slider> ();
		es [0].value -= energy;
	}

	public void increaseScrap (int scrapAmount)
	{
		Text[] st = GameObject.Find ("ST").GetComponents<Text> ();
		scrap += scrapAmount;
		st [0].text = scrap.ToString ();
	}

	public void increaseRkits ()
	{
		Text[] rt = GameObject.Find ("ST").GetComponents<Text> ();
		rKits += 1; 
		rt [0].text = rKits.ToString ();
	}

	public void useRepairKit ()
	{
		Slider[] hs = GameObject.Find ("HS").GetComponents<Slider> ();
		FindObjectOfType<SoundManagerScript> ().PlaySound ("repair");
		hs [0].value = hs[0].maxValue;
		rKits -= 1; 

	}
		


}
