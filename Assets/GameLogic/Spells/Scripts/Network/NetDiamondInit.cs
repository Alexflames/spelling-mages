using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetDiamondInit : NetworkBehaviour {
    private DiamondInit diamondInit;
    public GameObject diamond;

	void Awake () {
        if (isLocalPlayer)
        {
            CmdInitSpell();
            diamondInit.diamond = diamond;
        }
	}

    [Command]
    public void CmdInitSpell()
    {
        diamondInit = gameObject.AddComponent<DiamondInit>();
    }

    [Command]
    public void CmdCast()
    {
        diamondInit.cast(null);
    }
}
