using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetSplashInit : NetworkBehaviour, SpellInit
{
    // Variables for drawing circle
    public GameObject circleDrawer;
    private SplashCircleDrawer splashCircleDrawer;
    private string[] aliases = { "splash", "tsunami", "aqua strike", "tidal wave", "rogue wave" };
    [Range(0, 10)]
    public float radius = 8;

    // Name in the current game session
    public string SessionName = "";

    // Other variables
    public GameObject waterSplash;

    // Use this for initialization
    void Start()
    {
        SessionName = gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);

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
            string translation = "";
            switch (SessionName)
            {
                case "splash":
                    translation = "<color=#0000ffff>Всплеск</color>";
                    break;
                case "tsunami":
                    translation = "<color=#0000ffff>Цунами</color>";
                    break;
                case "aqua strike":
                    translation = "<color=#0000ffff>Водяной удар</color>";
                    break;
                case "tidal wave":
                    translation = "<color=#0000ffff>Приливная волна</color>";
                    break;
                case "rogue wave":
                    translation = "<color=#0000ffff>Волна-убийца</color>";
                    break;
                default:
                    break;
            }
            return translation + "(Splash) Требует нахождение <color=#0000ffff>источника воды</color> поблизости. Пустить волну в направление курсора";
		}
    }
}
