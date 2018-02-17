using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
	Rigidbody2D enemySpawner;
	Vector3 spawnEnemyLocation;
	public GameObject enemy;

	// Use this for initialization
	void Start ()
	{
		enemySpawner = GetComponent<Rigidbody2D> ();
		spawnEnemyLocation = enemySpawner.position;
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
			Instantiate (enemy, spawnEnemyLocation, Quaternion.identity);
			yield return new WaitForSeconds (5);
		}

	}
}
