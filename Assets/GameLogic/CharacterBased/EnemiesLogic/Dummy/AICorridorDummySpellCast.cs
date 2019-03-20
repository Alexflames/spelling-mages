using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICorridorDummySpellCast : MonoBehaviour {
	public float diamondPeriodicTime = 0;
	public float timeToCastDiamond = 2.0f;
	SpellCreating scComp;

	// Use this for initialization
	void Start () {
		scComp = gameObject.GetComponent<SpellCreating> ();
	}

	void FixedUpdate () {
		diamondPeriodicTime += Time.deltaTime;
		if (diamondPeriodicTime > timeToCastDiamond) {
			scComp.castSpell ("diamond");
			diamondPeriodicTime = 0;
		}
	}
}
