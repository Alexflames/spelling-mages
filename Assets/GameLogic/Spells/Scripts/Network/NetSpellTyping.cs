using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetSpellTyping : NetworkBehaviour
{

    public Text currentText;
    private NetSpellCreating spellCreateComponent;

    private GameObject newSpellBook;
    private Info hintLogic;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            currentText = GameObject.Find("TypeText").GetComponent<Text>();
            currentText.text = "";
            spellCreateComponent = GetComponentInParent<NetSpellCreating>();

            newSpellBook = GameObject.Find("NewSpellBook");

            hintLogic = newSpellBook.GetComponent<Info>();
            hintLogic.infoPanel.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F1) && currentText.text.Trim().Length >= 3 && newSpellBook.activeSelf)
        {
            hintLogic.TryActivate(currentText.text.Trim());
        }

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
