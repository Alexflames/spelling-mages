using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCreating : MonoBehaviour {

    public Text spellBookText;
	private GameObject playerCharacter;
    private Dictionary<string, SpellInit> spellbook = new Dictionary<string, SpellInit>();
    private System.Random rand = new System.Random();
    public bool randomise = false ;
    public void addSpell(string[] names, SpellInit sp)
    {
        
        string name = names[0];
        if (randomise)
        {
            name = names[rand.Next(names.Length)];
        }
        spellbook.Add(name, sp);
        if (spellBookText != null) spellBookText.text += "\n" + name;
    }

	// Use this for initialization
	void Start () {
        spellBookText = GameObject.Find("SpellBook").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Vector3 makeSpellSpawnPos(Vector3 adder, Transform owner) {
	//	return owner.position + adder;
	//}

	//Vector3 makeSpellSpawnPos(float x, float y, float z, Transform owner) {
	//	return owner.position + new Vector3 (x, y, z);
	//}
    
	public void castSpell (string name) {
        //print(spellbook.Count);
        if (spellbook.ContainsKey(name))
        {
            spellbook[name].cast();
        }
	}
    
}
