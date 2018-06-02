using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTyping : MonoBehaviour {

	public Text currentText;
	public List<string> spellBook;
	private SpellCreating spellCreateComponent;

	// Use this for initialization
	void Start () {
		currentText.text = "";
		spellCreateComponent = GetComponentInParent<SpellCreating>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) {
			string inputText = currentText.text.ToLower ();
			if (spellBook.Contains (inputText)) {
				spellCreateComponent.castSpell (inputText);
			}

			print ("Searching spell: " + inputText + ".\n");
			currentText.text = "";
		}
		else if (currentText.text.Length < 30)
			currentText.text += Input.inputString;
	}
}
