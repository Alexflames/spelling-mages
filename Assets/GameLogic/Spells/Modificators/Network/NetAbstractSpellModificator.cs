using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class NetAbstractSpellModificator : NetworkBehaviour, SpellModificator
{

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addModificator(this);
    }

    public abstract string Name { get; }
}
