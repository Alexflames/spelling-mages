using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCreating : MonoBehaviour {

    public Text spellBookText;
	private GameObject playerCharacter;
    private Dictionary<string, SpellInit> spellbook = new Dictionary<string, SpellInit>();
    private Dictionary<string, SpellModificator> modificators = new Dictionary<string, SpellModificator>();
    private System.Random rand = new System.Random();
    public bool randomise = false ;

    public void addModificator (SpellModificator sm){
        modificators.Add (sm.Name, sm);
    }

    public void addSpell(string[] names, SpellInit sp)
    {
        
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
        }
    }
    
	public SpellModificator getModIfExists (string name){
		if (name != null && modificators.ContainsKey(name)) return modificators[name];
		else return null;
	}
}
