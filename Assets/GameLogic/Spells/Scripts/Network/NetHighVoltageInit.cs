using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetHighVoltageInit : NetworkBehaviour, SpellInit
{
    public GameObject highVoltage;
    private string[] aliases = { "high voltage", "electricity", "lightning", "thunderstruck" };
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cast(string smName)
    {
        CmdCast(gameObject.transform.position + transform.forward, gameObject.transform.rotation, smName);
    }

    [Command]
    private void CmdCast(Vector3 pos, Quaternion rot, string smName)
    {
        GameObject voltage = GameObject.Instantiate(highVoltage, pos, rot);

        NetworkServer.Spawn(voltage);

        RpcOtherStuff(voltage, smName);
    }

    [ClientRpc]
    private void RpcOtherStuff(GameObject spell, string smName)
    {
        NetHighVoltageLogic voltageLogic = spell.GetComponent<NetHighVoltageLogic>();

        voltageLogic.ApplyModificator(null);
        voltageLogic.SetOwner(gameObject);

        SpellModificator sm = gameObject.GetComponent<NetSpellCreating>().getModIfExists(smName);
        voltageLogic.ApplyModificator(sm);
    }
}
