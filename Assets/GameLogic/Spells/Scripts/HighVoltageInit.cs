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

    private IEnumerator repeatCast (float wait, Vector3 spellSpawnPos, Quaternion rotation) {
        yield return new WaitForSeconds (wait);
        GameObject voltage2 = Instantiate (highVoltage, spellSpawnPos, rotation);
        voltage2.GetComponent<HighVoltageLogic>().SetOwner (gameObject);
    }

    public void cast (string smName)
    {
		SpellModificator sm = gameObject.GetComponent<SpellCreating> ().getModIfExists (smName);
        Vector3 spellSpawnPos = gameObject.transform.position + transform.forward;
        GameObject voltage = GameObject.Instantiate (highVoltage, spellSpawnPos, gameObject.transform.rotation);
        HighVoltageLogic voltageLogic = voltage.GetComponent<HighVoltageLogic>();
        if (sm != null && sm is RepeatModificator) {
             float wait = ((RepeatModificator)sm).wait;
             StartCoroutine (repeatCast (wait, spellSpawnPos, gameObject.transform.rotation));
        } else voltageLogic.ApplyModificator (sm);
        voltageLogic.SetOwner (gameObject);
    }

    public string Description {
		get {
			return "High voltage";
		}
    }
}
