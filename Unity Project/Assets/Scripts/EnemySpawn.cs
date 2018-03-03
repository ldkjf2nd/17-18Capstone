using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn
{
	public int enemyType;
	public int enemyAmount;

	public EnemySpawn ()
	{
        
	}

	public void spawnEnemy (int a, int b)
	{
		enemyType = a;
		enemyAmount = b;
	}

	public int enemySpawned ()
	{
		return enemyAmount;
	}
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
