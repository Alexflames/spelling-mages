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
    private string[] aliases = { "prediction", "forecast", "farseer" };
    [Range(5, 20)]
    public float lastingTime = 8;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void cast(string smName)
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

            
            CmdCast(transform.position + transform.forward, transform.rotation, gameObject.GetComponent<NetAICharacterControl>().hit.point);
            
            if (isLocalPlayer)
            {
                audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Call cast on server
    /// </summary>
    /// <param name="position">player position</param>
    /// <param name="rotation">player rotation</param>
    /// <param name="destination">move-to position of player</param>
    [Command]
    private void CmdCast(Vector3 position, Quaternion rotation, Vector3 destination)
    {
        spell = GameObject.Instantiate(prediction, position, rotation);

        NetworkServer.Spawn(spell);

        RpcOtherStuff(spell, destination);
    }

    [ClientRpc]
    private void RpcOtherStuff(GameObject spell, Vector3 destination)
    {
        NetPrediction_FateLogic spellLogic = spell.GetComponent<NetPrediction_FateLogic>();
        spellLogic.SetTimeLeft(lastingTime);
        spellLogic.SetOwner(gameObject);
        spellLogic.SetDestination(destination);
        this.spell = spell;
    }
    public string Description {
		get {
			return "netprediction";
		}
    }
}
