using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpellInit : MonoBehaviour, SpellInit {

	// Use this for initialization
	protected virtual void Start () {
		this.gameObject.GetComponent<SpellCreating>().addSpell(this);
	}

    public virtual void RMBReact() { }

    public abstract string[] Aliases{get; }
	public abstract string Description{get; }
	public abstract void cast (string smName);
    
}
