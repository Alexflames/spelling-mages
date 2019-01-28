using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetHighVoltageInit : NetworkBehaviour, SpellInit
{
	public GameObject highVoltage;
	private string[] aliases = { "high voltage", "electricity", "lightning", "thunderstruck", "supercharge", "coil overload" };

	// Name in the current game session
	public string SessionName = "";

	// Use this for initialization
	void Start()
	{
		SessionName = this.gameObject.GetComponent<NetSpellCreating>().addSpell(this);
	}

	public void cast(string smName)
	{
		CmdCast(gameObject.transform.position + transform.forward + transform.up * 0.25f, gameObject.transform.rotation, smName);
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
		
		voltageLogic.SetOwner(gameObject);

		SpellModificator sm = gameObject.GetComponent<NetSpellCreating>().getModIfExists(smName);
		voltageLogic.ApplyModificator(sm);
	}

	public string Description {
		get {
			string translation = "";
			switch (SessionName)
			{
				case "high voltage":
					translation = "<color=#e6b800ff>Высокое напряжение</color>";
					break;
				case "electricity":
					translation = "<color=#e6b800ff>Электричество</color>";
					break;
				case "lightning":
					translation = "<color=#e6b800ff>Молния</color>";
					break;
				case "thunderstruck":
					translation = "<color=#e6b800ff>\"Пораженный молнией\"</color>";
					break;
				case "supercharge":
					translation = "<color=#e6b800ff>Перезгрузить</color>";
					break;
				case "coil overload":
					translation = "<color=#e6b800ff>Перегрузка катушки</color>";
					break;
				default:
					break;
			}
			return translation + "(" + SessionName + ") " + "(high voltage) Создает <color=#e6b800ff>электрический разряд</color> в направлении взгляда персонажа.";
		}
	}


	public string[] Aliases {
		get {
			return aliases;
		}
	}
}
