using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
	public float time;

	// Use this for initialization
	void Start ()
	{
		time = Time.deltaTime;
		GameManager.level = 2;
	}

	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime; 
		if (time > 3) {
			Application.LoadLevel (GameManager.level);
		}
	}
}