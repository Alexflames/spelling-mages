using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetFateInit : NetAbstractSpellInit
{
	public NetPredictionInit predictionInit;
	public Image fateTransition;
	public string UIAnimName = "FateTransition";
	private string[] aliases = { "fate", "rewind" };

	// Name in the current game session
	public string SessionName = "";

	// Use this for initialization
	void Start()
	{
		SessionName = this.gameObject.GetComponent<NetSpellCreating>().addSpell(this);
		predictionInit = gameObject.GetComponent<NetPredictionInit>();
	}

	public override void cast(string smName)
	{
		if (predictionInit.spell)
		{
			predictionInit.spell.GetComponent<NetPrediction_FateLogic>().activateTransition();
			if (isLocalPlayer)
			{
				fateTransition = GameObject.Find(UIAnimName).GetComponent<Image>();
				fateTransition.color = new Color(1, 1, 1, 1);
				fateTransition.GetComponent<UIFateTransitionAnimation>().ActivateAnim();
				gameObject.GetComponent<AudioSource>().Stop();
			}
		}
	}

	public override string Description
	{
		get
		{
			string translation = "";
			switch (SessionName)
			{
				case "fate":
					translation = "<color=#7d00c8ff>Судьба</color>";
					break;
				case "rewind":
					translation = "<color=#7d00c8ff>Перемотка</color>";
					break;
				default:
					break;
			}
			return translation + "(" + SessionName + ") " + "(Fate) Перемещает  персонажа на место <color=#7d00c8ff>копии</color>. " +
				"Используйте <color=#7d00c8ff>prediction</color> для создания <color=#7d00c8ff>копии</color>";
		}
	}


	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
