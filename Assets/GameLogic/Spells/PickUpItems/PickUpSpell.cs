using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpell : MonoBehaviour {
	public String spellToAdd;
	private Type spellType;

	public void Start () {
		spellType = Type.GetType(spellToAdd);
	}

	void OnTriggerEnter (Collider collision) {
		SpellCreating sc = collision.gameObject.GetComponent<SpellCreating>();
		if (sc == null) return;
		collision.gameObject.AddComponent (spellType);
		UnityEngine.Object.Destroy (this.gameObject);
	}	
}
