using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantasmInit : MonoBehaviour, SpellInit {

    public GameObject phantasm;
    private string[] aliases = {"phantasm", "phantom" , "ghost"};
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
    }

    private IEnumerator repeatCast (float wait, Vector3 spellSpawnPos, Quaternion rotation) {
        yield return new WaitForSeconds (wait);
        GameObject spell = Instantiate(phantasm, spellSpawnPos, rotation);
        PhantasmLogic spellComp = spell.GetComponent<PhantasmLogic>();
        spellComp.SetOwner(gameObject);
    }

    public void cast (SpellModificator sm)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 phantasmDestination = hit.point;
            Vector3 spellSpawnPos = phantasmDestination + new Vector3(0, 1.0f, 0);
            GameObject spell = GameObject.Instantiate (phantasm, spellSpawnPos, gameObject.transform.rotation);
            PhantasmLogic spellComp = spell.GetComponent<PhantasmLogic>();
            if (sm != null && sm is RepeatModificator) {
                float wait = ((RepeatModificator)sm).wait;
                StartCoroutine (repeatCast (wait, spellSpawnPos,  gameObject.transform.rotation));
            } else spellComp.ApplyModificator (sm);
            spellComp.SetOwner(gameObject);
        }

    }
}
