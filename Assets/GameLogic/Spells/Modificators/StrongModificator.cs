using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongModificator:  AbstractSpellModificator {
	public float factor = 1.0f;
	public override string Name {
		get {
			return "strong";
		}
	}
}
