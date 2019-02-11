using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPhantasmInit : NetAbstractSpellInit
{
	public GameObject phantasm;
	private string[] aliases = { "phantasm", "phantom", "ghost", "spectre", "apparition" };

	// Name in the current game session
	public string SessionName = "";

	// Use this for initialization
	void Start()
	{
		SessionName = this.gameObject.GetComponent<NetSpellCreating>().addSpell(this);
	}

	[Command]
	private void CmdCast(Vector3 phantasmDestination, Quaternion playerRotation, string smName)
	{
		GameObject spell = GameObject.Instantiate(phantasm, phantasmDestination + new Vector3(0, 1.0f, 0), playerRotation);

		NetworkServer.Spawn(spell);

		RpcOtherStuff(spell, smName);
	}

	[ClientRpc]
	private void RpcOtherStuff(GameObject spell, string smName)
	{
		NetPhantasmLogic spellComp = spell.GetComponent<NetPhantasmLogic>();
		spellComp.SetOwner(gameObject);

		SpellModificator sm = gameObject.GetComponent<NetSpellCreating>().getModIfExists(smName);
		spellComp.ApplyModificator(sm);
	}

	public override void cast(string smName)
	{
		RaycastHit hit;

		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
		{
			CmdCast(hit.point, gameObject.transform.rotation, smName);
		}
	}

	public override string Description {
		get {
			string translation = "";
			switch (SessionName)
			{
				case "apparition":
					translation = "<b><color=#A67474ff>Призрак</color></b>";
					break;
				case "ghost":
					translation = "<b><color=#A67474ff>Призрак</color></b>";
					break;
				case "spectre":
					translation = "<b><color=#A67474ff>Привидение</color></b>";
					break;
				default:
					translation = "<b><color=#A67474ff>Фантом</color></b>";
					break;
			}
			return translation + "(" + SessionName + ") " + "Призывает <b><color=#A67474ff>тень</color></b>, " +
				"идущую в сторону персонажа. " +
				"Прикосновение с вами <color=#9d20e8ff>активирует</color> <b><color=#A67474ff>его</color></b>";
		}
	}

	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
