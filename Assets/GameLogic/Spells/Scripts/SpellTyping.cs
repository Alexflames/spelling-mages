using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && currentText.text.Trim().Length >= 3 && newSpellBook.activeSelf){
            hintLogic.TryActivate (currentText.text.Trim());
        }
	else
        if (Input.GetButtonDown("Submit") && Input.GetButton("Shift"))
        {
            string inputText = currentText.text.ToLower();
            hintLogic.Deactivate();
            inputText = inputText.Trim();
            spellCreateComponent.castSpell(inputText);

            print("Searching spell: " + inputText + ".\n");
            currentText.text = "";

        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && currentText.text.Length > 0)
        {
            hintLogic.Deactivate();
            currentText.text = currentText.text.Substring(0, currentText.text.Length - 1);
        }
        else if (!Input.GetKeyDown(KeyCode.Return) && currentText.text.Length < 30)
        {
            if (Input.inputString.Length > 0) hintLogic.Deactivate();
            currentText.text += Input.inputString;
        }
    }
}
