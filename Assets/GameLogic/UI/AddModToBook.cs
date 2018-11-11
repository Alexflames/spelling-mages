using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModToBook : MonoBehaviour {

	public GameObject templateEntry;
	
	public void addModBookEntry (string name, SpellModificator sm) {
		GameObject newEntry = Instantiate (templateEntry)  as GameObject;
		newEntry.GetComponent<SpellBookEntry>().setText (name);
		newEntry.transform.SetParent (gameObject.transform, false);
	}
}
