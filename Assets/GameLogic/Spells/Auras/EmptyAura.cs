using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyAura : MonoBehaviour, Aura {
	public virtual string Name {
		get {
			return "base";
		}
	}

	public virtual GameObject AuraModel
	{
		get; set;
	}

	public virtual void CastReaction() {	}	
}
