using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickModificator:  AbstractSpellModificator {
	public double speedFactor = 1.0;
	public double weakFactor = 1.0;
	public override string Name {
		get {
			return "quick";
		}
	}
}
