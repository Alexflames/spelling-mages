using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpellTyping : MonoBehaviour
{

	public Text currentText;
	private SpellCreating spellCreateComponent;
	public GameObject newSpellBook;
	private Info hintLogic;

	// Use this for initialization
	void Start()
	{
		currentText = GameObject.Find("TypeText").GetComponent<Text>();
		currentText.text = "";
		spellCreateComponent = GetComponentInParent<SpellCreating>();
		hintLogic =  newSpellBook.GetComponent<Info>();
	}
	static private char[] delimiters = {' '};
	// Update is called once per frame
	void Update()
	{
		string inputText = currentText.text.ToLower().Trim();
		string candidateSpellName = null, candidateModName = null;

		if (inputText.Length > 0) {
			candidateSpellName = spellCreateComponent.SearchSpell (inputText);
			if (candidateSpellName == null) {
				string[] inputParts = inputText.Split (delimiters, 2);
				candidateModName = spellCreateComponent.SearchSpell (inputParts[0]);
				if (inputParts.Length > 1) {
					candidateSpellName = spellCreateComponent.SearchSpell (inputParts[1]);
				}
			}
			if (currentText.text.Trim().Length >= 3  && candidateSpellName != null)
			{
				SpellInit hintS = spellCreateComponent.getSpellIfExists (candidateSpellName);
				if (hintS != null && hintS is SpellHintReaction) {
					((SpellHintReaction)hintS).onHintRequest();
				}
				hintLogic.ShowSpellDescription (candidateSpellName, hintS);
			}
		}
		

		if (Input.GetButtonDown("Submit") && Input.GetButton("Shift"))
		{
            
			//hintLogic.Deactivate();
			spellCreateComponent.castSpell(inputText);

			print("Searching spell: " + inputText + ".\n");
			currentText.text = "";
		}
		else if (Input.GetKeyDown(KeyCode.Backspace) && currentText.text.Length > 0)
		{
			//hintLogic.Deactivate();
			currentText.text = currentText.text.Substring(0, currentText.text.Length - 1);
		}
		else if (!Input.GetKeyDown(KeyCode.Return) && currentText.text.Length < 30 
		                                           && Input.inputString.Length == 1)
		{
			//if (Input.inputString.Length > 0) hintLogic.Deactivate();
			char ch = Input.inputString[0];
			if (ch == ' ' || Char.IsDigit(ch) || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
			currentText.text += ch;
		}
	}
}
