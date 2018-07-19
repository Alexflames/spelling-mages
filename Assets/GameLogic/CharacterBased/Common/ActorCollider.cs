using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCollider : MonoBehaviour {
    // Use this for initialization
    BuffDebuffController bdc;
	void Start () {
        bdc = GetComponent<BuffDebuffController>();
	}
	
	void OnTriggerStay(Collider collision)
    {
        // print("found " + collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Water":
                bdc.updateBuff("water-slow", 1.5f);
                break;
            default:
                break;
        }
    }
}
