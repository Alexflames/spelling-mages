using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FateInit : MonoBehaviour, SpellInit {
    public PredictionInit predictionInit;
    public Image fateTransition;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell("fate", this);
        predictionInit = gameObject.GetComponent<PredictionInit>();
    }

    public void cast()
    {
        if (predictionInit.spell)
        {
            Vector3 predictionPos = predictionInit.spell.transform.position;
            Destroy(predictionInit.spell);

            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(predictionPos);
            gameObject.transform.position = predictionPos + Vector3.up * 0.1f;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();

            fateTransition.gameObject.SetActive(true);


            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
