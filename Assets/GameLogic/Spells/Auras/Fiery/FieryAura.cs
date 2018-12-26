using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryAura : EmptyAura {
    public override string Name
    {
        get
        {
            return "Fiery";
        }
    }
    
    public override GameObject AuraModel { get; set; }

    public override void CastReaction()
    {
        Vector3 splashCentre;
    }
}
