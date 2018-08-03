using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionInit : MonoBehaviour, SpellInit {
    public GameObject prediction;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell("prediction", this);
    }
    
    public void cast()
    {
        print("noice!");
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 predictionDestination = hit.point;
            GameObject spell = GameObject.Instantiate(prediction, transform.position + transform.forward, transform.rotation);
            Prediction_FateLogic spellLogic = spell.GetComponent<Prediction_FateLogic>();
            spellLogic.SetOwner(gameObject);
        }
    }
}
