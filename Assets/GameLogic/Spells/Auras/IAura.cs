using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAura
{
    string name { get; }
    void CastReaction();
    // void HpLowerReaction(NetMortal hpCon);
}
