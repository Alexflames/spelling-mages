using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatModificator:  AbstractSpellModificator {
	public double scaleFactor = 1.0;
	public override string Name {
		get {
			return "great";
		}
	}
}
