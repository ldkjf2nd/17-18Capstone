using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	private int scoreCount;
	public Text scoreText;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void  UpdateScore ()
	{
		scoreCount = scoreCount + 1;
		scoreText.text = "Score: " + scoreCount.ToString ();
	}
}
