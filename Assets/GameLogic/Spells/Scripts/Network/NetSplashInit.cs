using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetSplashInit : NetworkBehaviour, SpellInit
{
    // Variables for drawing circle
    public GameObject circleDrawer;
    private SplashCircleDrawer splashCircleDrawer;
    private string[] aliases = { "splash", "tsunami", "killerwave", "aqua strike" };
    [Range(0, 10)]
    public float radius = 8;

    // Other variables
    public GameObject waterSplash;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);

        splashCircleDrawer = circleDrawer.GetComponent<SplashCircleDrawer>();
    }

    [Command]
    public void CmdCast(Vector3 waterDestination, Vector3 waterPos, string smName)
    {
        Quaternion towaradsPoint = Quaternion.LookRotation(waterDestination - waterPos + new Vector3(0, 0.75f, 0));
        var splash = GameObject.Instantiate(waterSplash, waterPos, towaradsPoint);

        NetworkServer.Spawn(splash);

        RpcOtherStuff(splash, smName);
    }

    [ClientRpc]
    private void RpcOtherStuff(GameObject spell, string smName)
    {
        NetSplashScript script = spell.GetComponent<NetSplashScript>();

        SpellModificator sm = gameObject.GetComponent<NetSpellCreating>().getModIfExists(smName);

        script.ApplyModificator(sm);
    }

    public void cast(string smName)
    {
        splashCircleDrawer.CreatePoints(radius);

        bool waterFound = false;
        Vector3 waterPos = new Vector3();
        Vector3 waterDestination;
        Collider[] intersectObjs = Physics.OverlapSphere(transform.position, radius);
        foreach (var obj in intersectObjs)
        {
            if (obj.tag == "Water")
            {
                waterFound = true;
                waterPos = obj.transform.position + new Vector3(0, 1.5f, 0);
                print("Water for splash casting found");
                break;
            }
        }

        if (waterFound)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                waterDestination = hit.point;

                CmdCast(waterDestination, waterPos, smName);
            }
        }
    }

    public string Description {
		get {
			return "netsplash";
		}
    }
}
