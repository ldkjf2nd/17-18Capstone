using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsBehaviour : MonoBehaviour {
	// script for function of settings menu inside pause
	public AudioSource sfx;
	public AudioSource bmg;

	public Dropdown resolutionDropdown;
	Resolution[] resolutions;

	void Start ()
	{

		sfx = GameObject.Find ("SoundManager").GetComponent<AudioSource>();		// sound source.
		bmg = GameObject.Find ("SoundManager").GetComponent<AudioSource>();

		int currentReso = 0;
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions ();

		List<string> options = new List<string> ();		// list of resolution sizes

		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);

			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentReso = i;
			}
		}
		resolutionDropdown.AddOptions (options);		
		resolutionDropdown.value = currentReso;		// display correct current resolution
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions [resolutionIndex];		//set resolution by index number of array
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetVolume (float volume)
	{
		sfx.volume = volume; 		//set volume by float volue from slider
		bmg.volume = volume;
	}

	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;	//set full screen on toggle
	}
}
