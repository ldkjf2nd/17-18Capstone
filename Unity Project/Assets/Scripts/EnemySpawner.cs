using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Transform enemy;

	// Use this for initialization
	void Start ()
	{
		SpawnEndlessEnemies ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void SpawnEndlessEnemies ()
	{
		StartCoroutine (SpawnEnemy ());
	}

	IEnumerator SpawnEnemy ()
	{
		while (true) {
			Instantiate (enemy, new Vector3 (53F, 4F, 0), Quaternion.identity);
			yield return new WaitForSeconds (5);
		}

	}
}
