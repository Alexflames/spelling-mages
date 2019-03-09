using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class NetAbstractSpellInit :  NetworkBehaviour, SpellInit {

    // Use this for initialization
    protected virtual void Start () {
		this.gameObject.GetComponent<NetSpellCreating>().addSpell(this);
	}

    public virtual void RMBReact() { }

    public abstract string[] Aliases{get; }
	public abstract string Description{get; }
	public abstract void cast (string smName);
}
