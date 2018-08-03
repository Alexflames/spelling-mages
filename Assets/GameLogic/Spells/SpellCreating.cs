using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreating : MonoBehaviour {
	
    
	private GameObject playerCharacter;
    private Dictionary<string, SpellInit> spellbook = new Dictionary<string, SpellInit>();

    public void addSpell(string name, SpellInit sp)
    {
        spellbook.Add(name, sp);
    }

	// Use this for initialization
	void Start () {
		
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
