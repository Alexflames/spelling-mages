using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatModificator:  AbstractSpellModificator {
	public float wait = 2.0f;
	public override string Name {
		get {
			return "repeat";
		}
	}
}
