using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour {
	public GameObject infoPanel;
	private Text header;
	private Text content;
	// Use this for initialization
	void Start () {
		int children = infoPanel.transform.childCount;
		for (int i = 0; i < children; ++i){
			Transform child = infoPanel.transform.GetChild (i);
			if (child.name == "Header"){
				header = child.gameObject.GetComponent<Text> ();
				continue;
			}
			if (child.name == "Content"){
				content = child.gameObject.GetComponent<Text> ();
			}
		}
	}
	
	void SetInfo (string name, string con) {
		infoPanel.SetActive(true);
		header.text = name;
		content.text = con;
	}

	public void TryActivate (string prename) {
		string sp = gameObject.GetComponent<AddSpellToBook>().Search (prename);
		if (sp != null){
			SetInfo (sp, gameObject.GetComponent<AddSpellToBook>().ReturnInit (sp).Description);
		}
	}

	public void Deactivate () {
		infoPanel.SetActive (false);
	}
}
