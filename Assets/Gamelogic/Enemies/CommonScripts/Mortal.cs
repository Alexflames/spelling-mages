using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour {
	public int startingHealth;
	public int health;

	void Awake () {
		if (startingHealth == 0) {
			startingHealth = 100;
		}
	}

	void Start () {
		health = startingHealth;
	}

	public void setStartingHealth(int value) {
		health = value;
	}

	public void lowerHP(int value) {
		health -= value;
		if (health < 1) {
			dies ();
		}
	}

	void dies() {
		print (gameObject.name + " is ded");
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
