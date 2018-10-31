using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPredictionInit : NetworkBehaviour, SpellInit
{
    public GameObject prediction;
    public GameObject spell;       // Created prediction object
    private AudioSource audioSource;
    public AudioClip clockSound;
    private string[] aliases = { "prediction", "sibylla" };
    [Range(5, 20)]
    public float lastingTime = 8;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void cast(SpellModificator sm)
    {
        if (!isLocalPlayer) return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 predictionDestination = hit.point;
            if (spell)
            {
                NetworkServer.Destroy(spell);
            }

            CmdCast(transform.position + transform.forward, transform.rotation);
            
            if (isLocalPlayer)
            {
                audioSource.Play();
            }
        }
    }

    [Command]
    private void CmdCast(Vector3 position, Quaternion rotation)
    {
        spell = GameObject.Instantiate(prediction, position, rotation);

        NetworkServer.Spawn(spell);

        RpcOtherStuff(spell);
    }

    [ClientRpc]
    private void RpcOtherStuff(GameObject spell)
    {
        NetPrediction_FateLogic spellLogic = spell.GetComponent<NetPrediction_FateLogic>();
        spellLogic.SetTimeLeft(lastingTime);
        spellLogic.SetOwner(gameObject);
    }

}
