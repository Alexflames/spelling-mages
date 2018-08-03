using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FateInit : MonoBehaviour, SpellInit {
    public PredictionInit predictionInit;


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
            gameObject.transform.position = predictionPos;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(gameObject.transform.position);
        }
    }
}
