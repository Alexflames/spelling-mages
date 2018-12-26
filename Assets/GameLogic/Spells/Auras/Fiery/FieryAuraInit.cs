using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryAuraInit : MonoBehaviour, SpellInit {

    private string[] aliases = { "ingious", "fiery" };
    public GameObject AuraModelPrefab;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
    }

    public void cast(string smName)
    {
        var auraModel = GameObject.Instantiate(AuraModelPrefab);
        var auCon = gameObject.GetComponent<AuraController>();
        auCon.CurrentAura = gameObject.AddComponent<FieryAura>();
        auCon.CurrentAura.AuraModel = auraModel;
    }

    public string Description
    {
        get
        {
            return "Fiery Aura";
        }
    }
}
