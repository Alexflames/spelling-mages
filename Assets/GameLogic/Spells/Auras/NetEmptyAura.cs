using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetEmptyAura : NetworkBehaviour, Aura
{
    public virtual string Name
    {
        get
        {
            return "base";
        }
    }

    public virtual GameObject AuraModel
    {
        get; set;
    }

    public virtual void CastReaction() { }
}
