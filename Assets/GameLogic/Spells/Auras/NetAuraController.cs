﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetAuraController : NetworkBehaviour
{
	private Aura CurrentAura { get; set; }
	private GameObject oldAura;
	// Use this for initialization
	void Awake()
	{
		CurrentAura = gameObject.AddComponent<NetEmptyAura>();
	}

	// Sets new aura and deletes old aura model 
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
			CurrentAura.AuraModel.transform.position = gameObject.transform.position;
		}
	}
}
