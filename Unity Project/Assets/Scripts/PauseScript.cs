using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	// script for pausing the game and pause menu
	public static bool gamePaused = false;
	public GameObject PauseMenuUI;


	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) { //action when pause button "esc" is pressed
			if (gamePaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	public void Resume()
	{
		PauseMenuUI.SetActive (false);  // when resume, deactivate UI canvas resume time scale.
		gamePaused = false;
		Time.timeScale = 1f;
	}

	public void Pause()		// when pause, activate UI and stop time scale.
	{
		PauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gamePaused = true;
	}
	public void Quit()		// action when exit button pressed
	{
		Application.Quit ();
	}
	public void MainMenu()	// action when back to main menu pressed
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

}
