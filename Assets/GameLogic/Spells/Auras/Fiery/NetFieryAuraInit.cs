﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetFieryAuraInit : NetAbstractSpellInit, Aura
{
	private string[] aliases = { "fiery", "igneous" };
	public GameObject AuraModelPrefab;
	public GameObject FieryBurst;

	public GameObject AuraModel { get; set; }

	public string Name
	{
		get
		{
			return "Fiery";
		}
	}

	// Invoked on local client (in SpellCreating)
	public override void cast(string smName)
	{
		var aura = GameObject.Instantiate(AuraModelPrefab);
		gameObject.GetComponent<NetAuraController>().SetAura(this, aura);
	}

	[Command]
	public void CmdSpawn(Vector3 pos, Quaternion rotata)
	{
		float numberOfShots = 8;
		float angle = 360 / numberOfShots;
		// Abstract object that sets the rotation of the outcoming fierybursts. 
		// The fieryburst prefab does NOTHING here
		GameObject formalRotation = Instantiate(FieryBurst, Vector3.up, new Quaternion()); 
		for (int i = 0; i < numberOfShots; i++)
		{
			var proj = GameObject.Instantiate(FieryBurst,
				gameObject.transform.position + formalRotation.transform.forward * 2 + Vector3.up,
				formalRotation.transform.rotation);
			formalRotation.transform.Rotate(new Vector3(0, angle, 0));
			NetworkServer.Spawn(proj);
		}
		GameObject.Destroy(formalRotation);
	}

	public void CastReaction()
	{
		CmdSpawn(gameObject.transform.position, gameObject.transform.rotation);
	}

	public override string Description
	{
		get
		{
			return "Fiery Aura";
		}
	}

	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
