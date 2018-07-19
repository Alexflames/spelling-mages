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

            // To remove extra spaces in the end of the word
            int strSize = inputText.Length;
            while (inputText[strSize - 1] == ' ')
            {
                strSize--;
            }
            inputText = inputText.Substring(0, strSize); // This does not actually work!
            if (spellBook.Contains(inputText))
            {
                spellCreateComponent.castSpell(inputText);
            }

            print("Searching spell: " + inputText + ".\n");
            currentText.text = "";
         
        }
        else if (!Input.GetKeyDown(KeyCode.Return) && currentText.text.Length < 30)
        {
            currentText.text += Input.inputString;
        }
    }
}
