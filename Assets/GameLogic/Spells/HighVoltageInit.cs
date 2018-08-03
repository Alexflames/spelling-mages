using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighVoltageInit : MonoBehaviour, SpellInit {
    public GameObject highVoltage;
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell("high voltage", this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void cast()
    {
        GameObject voltage = GameObject.Instantiate(highVoltage, gameObject.transform.position + transform.forward, gameObject.transform.rotation);
        HighVoltageLogic voltageLogic = voltage.GetComponent<HighVoltageLogic>();
        voltageLogic.SetOwner(gameObject);
    }
}
