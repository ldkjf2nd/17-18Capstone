using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
	private int scoreCount;
	public Text scoreText;
	public ScoreManager scoreManager;
	public Transform coin;


	// Use this for initialization
	void Start ()
	{
		SetScoreText ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void SetScoreText ()
	{
		scoreText.text = "Score: " + scoreCount.ToString ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player"))
			scoreManager.UpdateScore (); 
		this.gameObject.SetActive (false);
	}

}