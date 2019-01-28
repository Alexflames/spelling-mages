using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModBookLogic : MonoBehaviour {

	public GameObject templateEntry;
	private Dictionary<string, GameObject> entries = new Dictionary<string, GameObject>();
	private CanvasGroup cg;
	public void addModBookEntry (string name, SpellModificator sm) {
		GameObject newEntry = Instantiate (templateEntry)  as GameObject;
		newEntry.GetComponent<SpellBookEntry>().setText (name);
		newEntry.GetComponent<SpellBookEntry>().setSprite (name);
		newEntry.transform.SetParent (gameObject.transform, false);
		entries.Add (name, newEntry);
	}

	public void Start () {
		cg = this.gameObject.GetComponent<CanvasGroup>();
	}

	public void Reset () {
		foreach (string key in entries.Keys) {
			Destroy (entries[key]);
		}
		entries.Clear ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftControl)){
			cg.alpha = 1f - cg.alpha;
		}
	}

}
