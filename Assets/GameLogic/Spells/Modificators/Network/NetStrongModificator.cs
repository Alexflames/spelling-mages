using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetStrongModificator : NetAbstractSpellModificator
{
	public float factor = 1.0f;
	public override string Name
	{
		get
		{
			return "strong";
		}
	}
}
