using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FindObjectOfType<GameManager> ().resetRespawn ();
		FindObjectOfType<GameManager> ().levelReset ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
