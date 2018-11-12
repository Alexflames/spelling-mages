using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetFateInit : NetworkBehaviour, SpellInit
{
    public NetPredictionInit predictionInit;
    public Image fateTransition;
    public string UIAnimName = "FateTransition";
    private string[] aliases = { "fate" };
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<NetSpellCreating>().addSpell(aliases, this);
        predictionInit = gameObject.GetComponent<NetPredictionInit>();
    }

    public void cast(string smName)
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

    public string Description {
		get {
			return "netfate";
		}
    }
}
