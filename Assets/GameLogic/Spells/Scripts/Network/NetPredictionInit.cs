using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPredictionInit : NetAbstractSpellInit
{
	public GameObject prediction;
	public GameObject spell;	   // Created prediction object
	private AudioSource audioSource;
	public AudioClip clockSound;
	private string[] aliases = { "prediction", "forecast", "farseer" };
	[Range(5, 20)]
	public float lastingTime = 8;

	// Name in the current game session
	public string SessionName = "";

    // Use this for initialization
    protected override void Start()
	{
		SessionName = this.gameObject.GetComponent<NetSpellCreating>().addSpell(this);
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	public override void cast(string smName)
	{
		RaycastHit hit;

		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
		{
			if (spell)
			{
				NetworkServer.Destroy(spell);
			}
			
			CmdCast(transform.position + transform.forward, transform.rotation, gameObject.GetComponent<NetAICharacterControl>().hit.point);
			
			audioSource.Play();
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

        NetPrediction_FateLogic spellLogic = spell.GetComponent<NetPrediction_FateLogic>();
        spellLogic.SetTimeLeft(lastingTime);
        spellLogic.SetOwner(gameObject);
        spellLogic.SetDestination(destination);

        this.spell = spell;

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
	public override string Description {
		get
		{
			string translation = "";
			switch (SessionName)
			{
				case "prediction":
					translation = "<color=#7d00c8ff>Предсказание</color>";
					break;
				case "forecast":
					translation = "<color=#7d00c8ff>Прогноз</color>";
					break;
				case "farseer":
					translation = "<color=#7d00c8ff>Предсказатель</color>";
					break;
				default:
					break;
			}
			return translation + "(Prediction) Создает <color=#7d00c8ff>копию</color>, идущую в направлении движения." +
				" Используйте <color=#7d00c8ff>fate</color> для перемещения на место <color=#7d00c8ff>копии</color>";
		}
	}


	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
