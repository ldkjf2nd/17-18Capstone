using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	int health; 
	public Health(int health){
		this.health = health;
	}
	int removeHealth(int damage){
		health += health - damage;
		return health;
	}
	int addHealth(int health){
		health += health;
		return health; 
	}


}
