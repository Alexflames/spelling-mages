using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantasmInit : MonoBehaviour, SpellInit {

    public GameObject phantasm;
    private string[] aliases = {"phantasm", "phantom" , "ghost"};
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void cast()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 phantasmDestination = hit.point;
            GameObject spell = GameObject.Instantiate(phantasm, phantasmDestination + new Vector3(0, 1.0f, 0), gameObject.transform.rotation);
            PhantasmLogic spellComp = spell.GetComponent<PhantasmLogic>();
            spellComp.SetOwner(gameObject);
        }

    }
}
