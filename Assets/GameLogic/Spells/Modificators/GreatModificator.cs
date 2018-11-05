using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatModificator:  AbstractSpellModificator {
	public float scaleFactor = 1.0f;
	public override string Name {
		get {
			return "great";
		}
	}
}
