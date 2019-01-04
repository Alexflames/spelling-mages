using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetSpellCreating : NetworkBehaviour
{
    public Text spellBookText;
    private GameObject playerCharacter;
    private GameObject newSpellBookPanel;
    public GameObject modBookPanel;
    private Dictionary<string, SpellInit> spellbook = new Dictionary<string, SpellInit>();
    private Dictionary<string, SpellModificator> modificators = new Dictionary<string, SpellModificator>();
    private System.Random rand = new System.Random();
    public bool randomise = false;
    private NetAuraController auraController;

    public void addModificator(SpellModificator sm)
    {
        modificators.Add(sm.Name, sm);
        if (modBookPanel != null)
        {
            modBookPanel.GetComponent<ModBookLogic>().addModBookEntry(sm.Name, sm);
        }
    }

    public string addSpell(SpellInit sp)
    {
        string name = sp.Aliases[0];
        if (randomise)
        {
            name = sp.Aliases[rand.Next(sp.Aliases.Length)];
        }
        spellbook.Add(name, sp);
        if (newSpellBookPanel != null)
        {
            newSpellBookPanel.GetComponent<SpellBookLogic>().addSpellBookEntry(name, sp, sp.Aliases[0]);
        }
        return name;
    }

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            newSpellBookPanel = GameObject.Find("NewSpellBook");
            randomise = true;

            modBookPanel = GameObject.Find("ModBook");
            auraController = GetComponent<NetAuraController>();
        }
        
    }

    public void castSpell(string name)
    {
        //print(spellbook.Count);
        string smName = null;
        foreach (string key in modificators.Keys) {
            if (name.StartsWith(key)) {
                smName = key;
				name = name.Substring (key.Length).Trim ();
                break;
            }
        }
        if (spellbook.ContainsKey(name))
        {
            spellbook[name].cast(smName);
            if (auraController) auraController.ReactToCast();
        }
    }

    public SpellModificator getModIfExists(string name)
    {
        if (name != null && modificators.ContainsKey(name)) return modificators[name];
        else return null;
    }
}
