using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetDiamondInit : NetworkBehaviour, SpellInit
{
    public GameObject diamond;
    private Transform ownerTransform;
    private string[] aliases = { "diamond", "bullet" };
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    Vector3 makeSpellSpawnPos(Vector3 adder, Transform owner)
    {
        return owner.position + adder;
    }
    

    [Command]
    public void CmdCast(string smName)
    {
        SpawnLogic(smName);
    }

    [ClientRpc] 
    public void RpcCast(string smName)
    {
        SpawnLogic(smName);
    }

    public void SpawnLogic(string smName)
    {
        ownerTransform = this.gameObject.transform;
        Vector3 spellSpawnPosition = makeSpellSpawnPos(ownerTransform.forward * 2.0F, ownerTransform);
        spellSpawnPosition += new Vector3(0, 0.5f, 0);
        GameObject spawned = Instantiate(diamond, spellSpawnPosition, ownerTransform.rotation);
        NetworkServer.Spawn(spawned);

        RpcOtherStuff(spawned, smName);
    }

    [ClientRpc]
    public void RpcOtherStuff(GameObject spell, string smName)
    {
        gameObject.GetComponent<NetSpellCreating>().getModIfExists(smName);
    }

    public void cast(string smName)
    {
        if (isLocalPlayer)
        {
            CmdCast(smName);
        }
        else if (isServer) {
            SpawnLogic(smName);
        }
    }
}
