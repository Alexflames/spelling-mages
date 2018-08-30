using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCollider : MonoBehaviour {
    // Use this for initialization
    BuffDebuffController bdc;
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonMovement;
    void Start () {
        bdc = GetComponent<BuffDebuffController>();
        thirdPersonMovement = gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }
	
	void OnTriggerStay(Collider collision)
    {
        // print("found " + collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Water":
                if (bdc.getSameBuffs(Type.GetType("WaterSlowBuff")).Count == 0) { 
                    bdc.addBuff(new WaterSlowBuff(thirdPersonMovement, 1.5f));
                    //print("Add slow");
                }
                break;
            default:
                break;
        }
    }
}
