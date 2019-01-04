using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FateInit : MonoBehaviour, SpellInit {
    public PredictionInit predictionInit;
    public Image fateTransition;
    private string[] aliases = { "fate" };
    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell(this);
        predictionInit = gameObject.GetComponent<PredictionInit>();
    }

    public void cast(string smName)
    {
        if (predictionInit.spell)
        {
            predictionInit.spell.GetComponent<Prediction_FateLogic>().activateTransition();
            fateTransition.gameObject.SetActive(true);

            gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    public string Description {
		get {
			return "fate";
		}
    }

    public string[] Aliases {
        get {
            return aliases;
        }
    }
}
