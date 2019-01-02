using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour {
    public Aura CurrentAura { get; set; }
    private GameObject oldAura;
    void Awake()
    {
        CurrentAura = gameObject.AddComponent<EmptyAura>();
    }
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<EmptyAura>();
	}

    // Deletes old aura
    public void SetAura(Aura aura, GameObject model)
    {
        CurrentAura = aura;
        CurrentAura.AuraModel = model;
        Destroy(oldAura);
        oldAura = model;
    }

    public void ReactToCast()
    {
        CurrentAura.CastReaction();
    }
    
    void Update()
    {
        if (CurrentAura.AuraModel != null)
        {
            CurrentAura.AuraModel.transform.position = gameObject.transform.position ;
        }
    }
}
