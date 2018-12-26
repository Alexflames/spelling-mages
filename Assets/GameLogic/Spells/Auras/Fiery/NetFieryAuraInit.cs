using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetFieryAuraInit : NetworkBehaviour, SpellInit
{
    private string[] aliases = { "ignious", "fiery" };
    public GameObject AuraModelPrefab;
    public GameObject FieryBurst;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
    }

    public void cast(string smName)
    {
        gameObject.GetComponent<NetAuraController>()
            .SetAura(gameObject.AddComponent<NetFieryAura>(), GameObject.Instantiate(AuraModelPrefab));
    }

    public string Description
    {
        get
        {
            return "Fiery Aura";
        }
    }
}
