using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTyping : MonoBehaviour
{

    public Text currentText;
    public Text spellBookText;
    public List<string> spellBook;
    private SpellCreating spellCreateComponent;

    // Use this for initialization
    void Start()
    {
        currentText.text = "";
        spellCreateComponent = GetComponentInParent<SpellCreating>();

        foreach (string spell in spellBook)
        {
            spellBookText.text += "\n" + spell;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && Input.GetButton("Shift"))
        {
            string inputText = currentText.text.ToLower();

            inputText = inputText.Trim();
            spellCreateComponent.castSpell(inputText);

            print("Searching spell: " + inputText + ".\n");
            currentText.text = "";

        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && currentText.text.Length > 0)
        {
            currentText.text = currentText.text.Substring(0, currentText.text.Length - 1);
        }
        else if (!Input.GetKeyDown(KeyCode.Return) && currentText.text.Length < 30)
        {
            currentText.text += Input.inputString;
        }
    }
}
