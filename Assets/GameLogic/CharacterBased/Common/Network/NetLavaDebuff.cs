using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetLavaDebuff : Buff
{
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonMovement;
    NetMortal victim;
    // float time;

    public NetLavaDebuff(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter t)
    {
        thirdPersonMovement = t;
    }

    public void Init()
    {
        victim = thirdPersonMovement.GetComponent<NetMortal>();
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
