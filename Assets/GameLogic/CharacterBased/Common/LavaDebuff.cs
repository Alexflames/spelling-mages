using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDebuff : Buff {
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonMovement;
    Mortal victim;
   // float time;

    public LavaDebuff(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter t)
    {
        thirdPersonMovement = t;
    }

    public void Init()
    {
        victim = thirdPersonMovement.GetComponent<Mortal>();
    }
    
    public void Update()
    {
        victim.lowerHP(1);
    }

    public bool IsActive()
    {
        return true;
    }

    public void Destroy()
    {
    }
}
