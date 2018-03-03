using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swayingPlatform : MonoBehaviour
{
	private Vector2 startPosition;
	private Vector2 newPosition;

	public int speed = 3;
	public int maxDistanceX = 2;
	public int maxDistanceY = 2;


	void Start ()
	{
		startPosition = transform.position;
		newPosition = transform.position;
	}

	void Update ()
	{
		newPosition.x = startPosition.x + (maxDistanceX * Mathf.Sin (Time.time * speed));
		newPosition.y = startPosition.y + (maxDistanceY * Mathf.Sin (Time.time * speed));

		transform.position = newPosition;
	}
}
