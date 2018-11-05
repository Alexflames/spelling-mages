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

    public void cast(string sm)
    {
        CmdCast(gameObject.transform.position + transform.forward, gameObject.transform.rotation);
    }

    [Command]
    private void CmdCast(Vector3 pos, Quaternion rot)
    {
        GameObject voltage = GameObject.Instantiate(highVoltage, pos, rot);

        NetworkServer.Spawn(voltage);

        RpcOtherStuff(voltage);
    }

    [ClientRpc]
    private void RpcOtherStuff(GameObject spell)
    {
        NetHighVoltageLogic voltageLogic = spell.GetComponent<NetHighVoltageLogic>();
        voltageLogic.ApplyModificator(null);
        voltageLogic.SetOwner(gameObject);
    }
}
