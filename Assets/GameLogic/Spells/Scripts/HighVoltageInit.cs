using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighVoltageInit : MonoBehaviour, SpellInit {
    public GameObject highVoltage;
    private string[] aliases = { "high voltage", "electricity", "lightning", "thunderstruck" };
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void cast(SpellModificator sm)
    {
        GameObject voltage = GameObject.Instantiate(highVoltage, gameObject.transform.position + transform.forward, gameObject.transform.rotation);
        HighVoltageLogic voltageLogic = voltage.GetComponent<HighVoltageLogic>();
        voltageLogic.ApplyModificator (sm);
        voltageLogic.SetOwner(gameObject);
    }
}
