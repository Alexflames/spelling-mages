using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPhantasmInit : NetworkBehaviour, SpellInit
{
    public GameObject spellTESTTEST;
    //public SpellModificator nextSpellModificator;
    public GameObject nextSpellOwner;

    public GameObject phantasm;
    private string[] aliases = { "phantasm", "phantom", "ghost" };
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    private void CmdCast(Vector3 phantasmDestination, Quaternion playerRotation)
    {
        GameObject spell = GameObject.Instantiate(phantasm, phantasmDestination + new Vector3(0, 1.0f, 0), playerRotation);

        NetworkServer.Spawn(spell);

        RpcOtherStuff(spell);
    }

    [ClientRpc]
    private void RpcOtherStuff(GameObject spell)
    {
        
        NetPhantasmLogic spellComp = spell.GetComponent<NetPhantasmLogic>();
        print(gameObject.GetInstanceID());
        spellComp.SetOwner(gameObject);
        //spellComp.ApplyModificator(nextSpellModificator);
        //nextSpellModificator = null;
        nextSpellOwner = null;
    }

    public void cast(string smName)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 phantasmDestination = hit.point;
            //nextSpellModificator = sm;
            nextSpellOwner = gameObject;

            // print("local player. ok!");
            CmdCast(phantasmDestination, gameObject.transform.rotation);

            //Apply modificator
        }

    }
}
