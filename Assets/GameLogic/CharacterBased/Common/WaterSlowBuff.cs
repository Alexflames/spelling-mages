using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlowBuff : Buff {
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonMovement;
    float time;

    public WaterSlowBuff(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter t, float tim)
    {
        thirdPersonMovement = t;
        time = tim;
    }

	public void Init () {
        thirdPersonMovement.ChangeSpeed(0.7f);
    }
	
	// Update is called once per frame
	public void Update () {
        time = time - Time.deltaTime;
	}

    public bool IsActive()
    {
        return time > 0;
    }

    public void Destroy()
    {
        thirdPersonMovement.ChangeSpeed(1 / 0.7f);
    }
}
