using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetSpellCreating : NetworkBehaviour
{
    public Text spellBookText;
    private GameObject playerCharacter;
    private Dictionary<string, SpellInit> spellbook = new Dictionary<string, SpellInit>();
    private Dictionary<string, SpellModificator> modificators = new Dictionary<string, SpellModificator>();
    private System.Random rand = new System.Random();
    public bool randomise = false;

    public void addModificator(SpellModificator sm)
    {
        modificators.Add(sm.Name, sm);
    }

    public void addSpell(string[] names, SpellInit sp)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        string name = names[0];
        if (randomise)
        {
            name = names[rand.Next(names.Length)];
        }
        spellbook.Add(name, sp);
        if (spellBookText != null)
        {
            spellBookText.text += "\n" + name;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            spellBookText = GameObject.Find("SpellBook").GetComponent<Text>();
            spellBookText.text = "SpellBook \n";
            randomise = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void castSpell(string name)
    {
        //print(spellbook.Count);
        SpellModificator sm = null;
        foreach (string key in modificators.Keys)
        {
            if (name.StartsWith(key))
            {
                sm = modificators[key];
                name = name.Substring(key.Length).Trim();
                break;
            }
        }
        if (spellbook.ContainsKey(name))
        {
            spellbook[name].cast(sm);
        }
    }

}
