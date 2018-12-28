using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetAuraController : NetworkBehaviour
{
    private Aura CurrentAura { get; set; }
    // Use this for initialization
    void Awake()
    {
        CurrentAura = gameObject.AddComponent<NetEmptyAura>();
    }

    // Deletes old aura
    public void SetAura(Aura aura, GameObject model)
    {
        CurrentAura = aura;
        CurrentAura.AuraModel = model;
    }

    public void ReactToCast()
    {
        CurrentAura.CastReaction();
    }

    void Update()
    {
        if (CurrentAura.AuraModel != null)
        {
            CurrentAura.AuraModel.transform.position = gameObject.transform.position;
        }
    }
}
