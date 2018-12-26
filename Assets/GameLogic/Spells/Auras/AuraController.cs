using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour {
    public Aura CurrentAura { get; set; }
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<EmptyAura>();
	}

    // Deletes old aura
    void SetAura(Aura aura)
    {
        Destroy(gameObject.GetComponent<EmptyAura>());
        CurrentAura = aura;
    }
    
    void Update()
    {
        if (CurrentAura.AuraModel != null)
        {
            CurrentAura.AuraModel.transform.position = gameObject.transform.position ;
        }
    }
}
