using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreating : MonoBehaviour {
	public GameObject diamond;
    public GameObject waterSplash;
    public GameObject highVoltage;
    public GameObject phantasm;
	private GameObject playerCharacter;
	private Transform ownerTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 makeSpellSpawnPos(Vector3 adder, Transform owner) {
		return owner.position + adder;
	}

	Vector3 makeSpellSpawnPos(float x, float y, float z, Transform owner) {
		return owner.position + new Vector3 (x, y, z);
	}
    
	public void castSpell (string name) {
		switch (name) {
		    case "diamond":
			    castDiamond ();
			    break;
            case "splash":
                castSplash();
                break;
            case "high voltage":
                castHighVoltage();
                break;
            case "phantasm":
                castPhantasm();
                break;
		    default:
			    break;
		}
	}

	void castDiamond() {
		ownerTransform = this.gameObject.transform;
		Vector3 spellSpawnPosition = makeSpellSpawnPos (ownerTransform.forward * 2.0F, ownerTransform);
		spellSpawnPosition += new Vector3 (0, 0.5f, 0);
		Instantiate (diamond, spellSpawnPosition, ownerTransform.rotation);
	}

    void castSplash()
    {
        bool waterFound = false;
        Vector3 waterPos = new Vector3();
        Vector3 waterDestination;
        Collider[] intersectObjs = Physics.OverlapSphere(transform.position, 8.0f);
        foreach(var obj in intersectObjs)
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

    void castHighVoltage()
    {
        GameObject voltage = GameObject.Instantiate(highVoltage, gameObject.transform.position + transform.forward, gameObject.transform.rotation);
        HighVoltageLogic voltageLogic = voltage.GetComponent<HighVoltageLogic>();
        voltageLogic.SetOwner(gameObject);
    }

    void castPhantasm()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 phantasmDestination = hit.point;
            GameObject spell = GameObject.Instantiate(phantasm, phantasmDestination + new Vector3(0, 1.0f, 0), gameObject.transform.rotation);
            PhantasmLogic spellComp = spell.GetComponent<PhantasmLogic>();
            spellComp.SetOwner(gameObject);
        }
        
    }
}
