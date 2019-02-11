using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FateInit : AbstractSpellInit {
	public PredictionInit predictionInit;
	public Image fateTransition;
	private string[] aliases = { "fate" };
	// Use this for initialization
	protected override void Start () {
		this.gameObject.GetComponent<SpellCreating>().addSpell(this);
		predictionInit = gameObject.GetComponent<PredictionInit>();
	}

	public override void cast(string smName)
	{
		if (predictionInit.spell)
		{
			predictionInit.spell.GetComponent<Prediction_FateLogic>().activateTransition();
			fateTransition.gameObject.SetActive(true);

			gameObject.GetComponent<AudioSource>().Stop();
		}
	}

	public override string Description {
		get {
			return "fate";
		}
	}

	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
