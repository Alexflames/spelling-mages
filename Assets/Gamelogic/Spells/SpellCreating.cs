using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreating : MonoBehaviour {
	public GameObject diamond;
	private GameObject playerCharacter;
	private Transform ownerTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 makeSpellSpawnPos(Vector3 adder, Transform owner) {
		return owner.position + adder;
	}

	Vector3 makeSpellSpawnPos(float x, float y, float z, Transform owner) {
		return owner.position + new Vector3 (x, y, z);
	}

	public void castSpell (string name) {
		switch (name) {
		case "diamond":
			castDiamond ();
			break;
		default:
			break;
		}
	}

	void castDiamond() {
		ownerTransform = this.gameObject.transform;
		Vector3 spellSpawnPosition = makeSpellSpawnPos (ownerTransform.forward * 2.0F, ownerTransform);
		spellSpawnPosition += new Vector3 (0, 0.5f, 0);
		Instantiate (diamond, spellSpawnPosition, ownerTransform.rotation);
	}
}
