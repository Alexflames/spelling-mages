using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModToBook : MonoBehaviour {

	public GameObject templateEntry;
	private Dictionary<string, SpellBookEntry> entries = new Dictionary<string, SpellBookEntry>();
	public void addModBookEntry (string name, SpellModificator sm) {
		GameObject newEntry = Instantiate (templateEntry)  as GameObject;
		newEntry.GetComponent<SpellBookEntry>().setText (name);
		newEntry.transform.SetParent (gameObject.transform, false);
		entries.Add (name, newEntry.GetComponent<SpellBookEntry>());
	}

	public void Reset () {
		foreach (string key in entries.Keys) {
			Destroy (entries[key]);
		}
		entries.Clear ();
	}
}
