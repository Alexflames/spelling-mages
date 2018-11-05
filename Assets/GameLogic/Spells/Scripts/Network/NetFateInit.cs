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
        fateTransition = GameObject.Find(UIAnimName).GetComponent<Image>();
    }

    public void cast(string smName)
    {
        if (predictionInit.spell)
        {
            predictionInit.spell.GetComponent<NetPrediction_FateLogic>().activateTransition();
            // fateTransition.gameObject.SetActive(true);

            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
