using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondInit : MonoBehaviour, SpellInit {
    public GameObject diamond;
    private Transform ownerTransform;

    // Use this for initialization
    void Start () {
       this.gameObject.GetComponent<SpellCreating>().addSpell("diamond", this);
	}
	
	// Update is called once per frame
	void Update () {
    }

    Vector3 makeSpellSpawnPos(Vector3 adder, Transform owner)
    {
        return owner.position + adder;
    }

    public void cast()
    {
        ownerTransform = this.gameObject.transform;
        Vector3 spellSpawnPosition = makeSpellSpawnPos(ownerTransform.forward * 2.0F, ownerTransform);
        spellSpawnPosition += new Vector3(0, 0.5f, 0);
        Instantiate(diamond, spellSpawnPosition, ownerTransform.rotation);
    }
}
