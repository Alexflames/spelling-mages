using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Aura
{
	string Name { get; }
	GameObject AuraModel { get; set; }
	void CastReaction();
	// void HpLowerReaction(NetMortal hpCon);
}
