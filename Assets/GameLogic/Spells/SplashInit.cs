using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInit : MonoBehaviour, SpellInit {
    public GameObject waterSplash;
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell("splash", this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void cast()
    {
        bool waterFound = false;
        Vector3 waterPos = new Vector3();
        Vector3 waterDestination;
        Collider[] intersectObjs = Physics.OverlapSphere(transform.position, 8.0f);
        foreach (var obj in intersectObjs)
        {
            if (obj.tag == "Water")
            {
                waterFound = true;
                waterPos = obj.transform.position + new Vector3(0, 1.5f, 0);
                print("Water for splash casting found");
                break;
            }
        }

        if (waterFound)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                waterDestination = hit.point;
                Quaternion towaradsPoint = Quaternion.LookRotation(waterDestination - waterPos + new Vector3(0, 0.75f, 0));
                GameObject.Instantiate(waterSplash, waterPos, towaradsPoint);
            }
        }
    }
}
