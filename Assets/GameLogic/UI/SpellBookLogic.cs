using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookLogic: MonoBehaviour {

	public GameObject templateEntry;
	private CanvasGroup cg;
	private Dictionary<string, GameObject> entries = new Dictionary<string, GameObject>();
	public void addSpellBookEntry (string name, SpellInit spell) {
		GameObject newEntry = Instantiate (templateEntry);
		newEntry.GetComponent<SpellBookEntry>().setText (name);
		newEntry.GetComponent<SpellBookEntry>().setSprite (spell.Aliases[0]);
		newEntry.transform.SetParent (gameObject.transform, false);
		entries.Add (name, newEntry);
	}


	public void Start () {
		cg = this.gameObject.GetComponent<CanvasGroup>();
	}

	public void Reset () {
		foreach (string key in entries.Keys) {
			print("i wanna reset spellbook");
			Destroy (entries[key]);
		}
		entries.Clear ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			cg.alpha = 1f - cg.alpha;
		}
	}

}
