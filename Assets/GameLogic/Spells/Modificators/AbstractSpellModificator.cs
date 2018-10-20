using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpellModificator : MonoBehaviour, SpellModificator {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpellCreating>().addModificator(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public abstract string Name{get; }
}
