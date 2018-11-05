using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetStrongModificator : NetAbstractSpellModificator
{
    public double factor = 1.0;
    public override string Name
    {
        get
        {
            return "strong";
        }
    }
}
