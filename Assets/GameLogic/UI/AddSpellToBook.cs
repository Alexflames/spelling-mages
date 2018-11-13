using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpellToBook : MonoBehaviour {

	public GameObject templateEntry;
	private Dictionary<string, SpellInit> spells = new Dictionary<string, SpellInit>();
	private Dictionary<string, GameObject> entries = new Dictionary<string, GameObject>();
	public void addSpellBookEntry (string name, SpellInit spell) {
		GameObject newEntry = Instantiate (templateEntry);
		newEntry.GetComponent<SpellBookEntry>().setText (name);
		newEntry.transform.SetParent (gameObject.transform, false);
		spells.Add (name, spell);
		entries.Add (name, newEntry);
	}

	public string Search (string prename){
		foreach (string key in spells.Keys) {
			if (key.StartsWith (prename)) return key;
		}
		return null;
	}

	public void Reset () {
        
		foreach (string key in entries.Keys) {
            print("i wanna reset spellbook");
            Destroy (entries[key]);
		}
		spells.Clear ();
		entries.Clear ();
	}

	public SpellInit ReturnInit (string name) {
		return spells[name];
	}
}
