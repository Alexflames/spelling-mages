using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetSpellTyping : NetworkBehaviour
{

	public Text currentText;
	private TypeTextSpellCheck spellCheck;
	private NetSpellCreating spellCreateComponent;

	private GameObject newSpellBook;
	private Info hintLogic;

	void Awake(){}

	// Use this for initialization
	void Start()
	{
		if (isLocalPlayer)
		{
			currentText = GameObject.Find("TypeText").GetComponent<Text>();
			spellCheck = GameObject.Find("TypeText").GetComponent<TypeTextSpellCheck>();
			currentText.text = "";
			spellCreateComponent = GetComponentInParent<NetSpellCreating>();

			newSpellBook = GameObject.Find("NewSpellBook");

			hintLogic = newSpellBook.GetComponent<Info>();
			hintLogic.infoPanel.SetActive(false);
		}
	}

	static private char[] delimiters = {' '};
	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		string inputText = currentText.text.ToLower().Trim();
		string candidateSpellName = null, candidateModName = null;
		bool typo = false;
		if (inputText.Length > 0) {
			candidateSpellName = spellCreateComponent.SearchSpell (inputText);
			if (candidateSpellName == null) {
				string[] inputParts = inputText.Split (delimiters, 2);
				candidateModName = spellCreateComponent.SearchMod (inputParts[0]);
				if (inputParts.Length > 1) {
					candidateSpellName = spellCreateComponent.SearchSpell (inputParts[1]);
					if (candidateSpellName == null) typo = true;
				}
				if (candidateModName == null) typo = true;
			}
			if (currentText.text.Trim().Length >= 3 && candidateSpellName != null)
			{
				SpellInit hintS = spellCreateComponent.getSpellIfExists (candidateSpellName);
				if (hintS != null && hintS is SpellHintReaction) {
					((SpellHintReaction)hintS).onHintRequest();
				}
				hintLogic.ShowSpellDescription (candidateSpellName, hintS);
			}
		}
		
		if (typo) spellCheck.Alert (); else spellCheck.Unalert ();

		if (Input.GetButtonDown("Submit") && Input.GetButton("Shift"))
		{
			spellCreateComponent.castSpell(inputText);

			print("Searching spell: " + inputText + ".\n");
			currentText.text = "";
		}
		else if (Input.GetKeyDown(KeyCode.Backspace) && currentText.text.Length > 0)
		{
			currentText.text = currentText.text.Substring(0, currentText.text.Length - 1);
		}
		else if (!Input.GetKeyDown(KeyCode.Return) && currentText.text.Length < 30
		                                           && Input.inputString.Length == 1)
		{
			char ch = Input.inputString[0];
			if (ch == ' ' || Char.IsDigit(ch) || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
				currentText.text += ch;
		}
	}
}
