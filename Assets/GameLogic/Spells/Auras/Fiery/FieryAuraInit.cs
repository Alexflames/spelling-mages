﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryAuraInit : MonoBehaviour, SpellInit, Aura {

    private string[] aliases = { "ignious", "fiery" };
    public GameObject AuraModelPrefab;
    public GameObject FieryBurst;

    public GameObject AuraModel { get; set; }

    public string Name
    {
        get
        {
            return "Fiery";
        }
    }


    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
    }

    public void cast(string smName)
    {
        var aura = GameObject.Instantiate(AuraModelPrefab);
        gameObject.GetComponent<AuraController>().SetAura(this, aura);
    }

    
    public void CmdSpawn(Vector3 pos, Quaternion rotata)
    {
        float numberOfShots = 8;
        float angle = 360 / numberOfShots;
        // Abstract object that sets the rotation of the outcoming fierybursts. 
        // The fieryburst prefab does NOTHING here
        GameObject formalRotation = Instantiate(FieryBurst, Vector3.up, new Quaternion()); 
        for (int i = 0; i < numberOfShots; i++)
        {
            var proj = GameObject.Instantiate(FieryBurst,
                gameObject.transform.position + formalRotation.transform.forward * 2 + Vector3.up,
                formalRotation.transform.rotation);
            formalRotation.transform.Rotate(new Vector3(0, angle, 0));
        }
        GameObject.Destroy(formalRotation);
    }

    public void CastReaction()
    {
        CmdSpawn(gameObject.transform.position, gameObject.transform.rotation);
    }


    public string Description
    {
        get
        {
            return "Fiery Aura";
        }
    }
}
