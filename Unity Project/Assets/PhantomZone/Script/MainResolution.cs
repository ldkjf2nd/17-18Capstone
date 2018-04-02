using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainResolution : MonoBehaviour {

	// Use this for initialization

	public Dropdown resolutionDropdown;
	Resolution[] resolutions;

	void Start ()
	{
		int currentReso = 0;
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions ();

		List<string> options = new List<string> (); //get list of resolution options.

		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);

			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentReso = i;
			}
		}
		resolutionDropdown.AddOptions (options);
		resolutionDropdown.value = currentReso;			// display correct resolution currently.
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions [resolutionIndex];		// set resolution to index of number of droplist.
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}


	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
}
