using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetFieryAuraInit : NetAbstractSpellInit, Aura
{
	private string[] aliases = { "fiery", "igneous" };
	private GameObject AuraModelPrefab;
	private GameObject FieryBurst;

    public string SessionName = "";

	public GameObject AuraModel { get; set; }

    protected override void Start()
    {
        SessionName = this.gameObject.GetComponent<NetSpellCreating>().addSpell(this);
        AuraModelPrefab = Resources.Load("NetFieryAuraContainer", typeof(GameObject)) as GameObject;
        FieryBurst = Resources.Load("NetFieryBurst", typeof(GameObject)) as GameObject;
    }

	public string Name
	{
		get
		{
			return "Fiery";
		}
	}

	// Invoked on local client (in SpellCreating)
	public override void cast(string smName)
	{
        CmdCreateAura();
	}

    [Command]
    void CmdCreateAura()
    {
        RpcCreateAura();
    }

    [ClientRpc]
    void RpcCreateAura()
    {
        var aura = GameObject.Instantiate(AuraModelPrefab);
        gameObject.GetComponent<NetAuraController>().SetAura(this, aura);
    }

	[Command]
	public void CmdSpawn(Vector3 pos, Quaternion rotata)
	{
		float numberOfShots = 8;
		float angle = 360 / numberOfShots;
		// Abstract object that sets the rotation of the outcoming fierybursts. 
		// The fieryburst prefab does NOTHING here
		GameObject formalRotation = Instantiate(FieryBurst, Vector3.up, new Quaternion()); 
		for (int i = 0; i < numberOfShots; i++)
		{
			var proj = GameObject.Instantiate(FieryBurst,
				gameObject.transform.position + formalRotation.transform.forward * 2 + Vector3.up,
				formalRotation.transform.rotation);
			formalRotation.transform.Rotate(new Vector3(0, angle, 0));
			NetworkServer.Spawn(proj);
		}
		GameObject.Destroy(formalRotation);
	}

	public void CastReaction()
	{
        if (isLocalPlayer)
        {
            CmdSpawn(gameObject.transform.position, gameObject.transform.rotation);
        }
	}

	public override string Description
	{
		get
		{
			return "Fiery Aura";
		}
	}

	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
