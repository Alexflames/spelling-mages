using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCreating : MonoBehaviour {

    public Text spellBookText;
	public GameObject newSpellBookPanel;
	public GameObject modBookPanel;
	private GameObject playerCharacter;
    private Dictionary<string, SpellInit> spellbook = new Dictionary<string, SpellInit>();
    private Dictionary<string, SpellModificator> modificators = new Dictionary<string, SpellModificator>();
    private System.Random rand = new System.Random();
    public bool randomise = false ;
    private AuraController auraController;

    public void Start(){
         auraController = GetComponent<AuraController>();
    }

    public void addModificator (SpellModificator sm){
        modificators.Add (sm.Name, sm);
		if(modBookPanel != null) {
			modBookPanel.GetComponent<ModBookLogic>().addModBookEntry (sm.Name, sm);
		}
    }

    public void addSpell(SpellInit sp)
    {
        
        string name = sp.Aliases[0];
        if (randomise)
        {
            name = sp.Aliases[rand.Next(sp.Aliases.Length)];
        }
        spellbook.Add(name, sp);
		if(newSpellBookPanel != null) {
			newSpellBookPanel.GetComponent<SpellBookLogic>().addSpellBookEntry (name, sp);
		}
    }
   
    public void castSpell (string name) {
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
    
	public SpellModificator getModIfExists (string name){
		if (name != null && modificators.ContainsKey(name)) return modificators[name];
		else return null;
	}

	public SpellInit getSpellIfExists (string name){
		if (name != null && spellbook.ContainsKey(name)) return spellbook[name];
		else return null;
	}

	public string SearchSpell (string prename){
		foreach (string key in spellbook.Keys) {
			if (key.StartsWith (prename)) return key;
		}
		return null;
	}

	public string SearchMod (string prename){
		foreach (string key in modificators.Keys) {
			if (key.StartsWith (prename)) return key;
		}
		return null;
	}
}
