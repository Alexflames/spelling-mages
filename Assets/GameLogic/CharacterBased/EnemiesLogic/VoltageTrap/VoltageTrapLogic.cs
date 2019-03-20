using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltageTrapLogic : MonoBehaviour {
    public float timeToCast = 2.0f;
    public float time;
    public float radius;
    SpellCreating scComp;
    // Use this for initialization
    void Start () {
        scComp = gameObject.GetComponent<SpellCreating>();
    }
	
	// Update is called once per frame
	void Update () {
		if(time < timeToCast)
        {
            time += Time.deltaTime;
        }
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, radius);
        if (hitColliders.Length != 0)
        {
            Collider oneToKill = null;
            foreach (var obj in hitColliders)
            {
                if (obj.CompareTag("Destroyable") && obj.gameObject != this.gameObject)
                {
                    oneToKill = obj;
                    break;
                }
            }
            if (oneToKill == null) return;
            this.gameObject.transform.LookAt(oneToKill.transform);
            if (time >= timeToCast) {
                print("cast");
                scComp.castSpell("high voltage");
                time = 0;
            }
        }
    }
}
