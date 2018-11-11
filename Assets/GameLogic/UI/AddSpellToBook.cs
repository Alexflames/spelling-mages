using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpellToBook : MonoBehaviour {

	public GameObject templateEntry;
	
	public void addSpellBookEntry (string name, SpellInit spell) {
		GameObject newEntry = Instantiate (templateEntry)  as GameObject;
		newEntry.GetComponent<SpellBookEntry>().setText (name);
		newEntry.transform.SetParent (gameObject.transform, false);
	}
}
