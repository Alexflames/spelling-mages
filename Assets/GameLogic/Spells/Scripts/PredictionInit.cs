using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionInit : MonoBehaviour, SpellInit {
    public GameObject prediction;
    public GameObject spell;       // Created prediction object
    private AudioSource audioSource;
    public AudioClip clockSound;
    private string[] aliases = { "prediction", "sibylla" };
    [Range(5, 20)]
    public float lastingTime = 8;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    public void cast(string smName)
    {
        print("noice!");
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 predictionDestination = hit.point;
            if (spell)
            {
                Destroy(spell);
            }
            spell = GameObject.Instantiate(prediction, transform.position + transform.forward, transform.rotation);
            Prediction_FateLogic spellLogic = spell.GetComponent<Prediction_FateLogic>();
            spellLogic.SetTimeLeft(lastingTime);
            spellLogic.SetOwner(gameObject);
            audioSource.Play();
        }
    }

    public string Description {
		get {
			return "prediction";
		}
    }

}
