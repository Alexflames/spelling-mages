using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInit : AbstractSpellInit, SpellHintReaction {
	// Variables for drawing circle
	public GameObject circleDrawer;
	private SplashCircleDrawer splashCircleDrawer;
	private string[] aliases = { "splash", "tsunami", "killerwave", "aqua strike"};
	[Range(0, 10)]
	public float radius = 8;

	// Other variables
	public GameObject waterSplash;

    // Use this for initialization
    protected override void Start () {
		this.gameObject.GetComponent<SpellCreating>().addSpell(this);

		splashCircleDrawer = circleDrawer.GetComponent<SplashCircleDrawer>();
	}

	public void onHintRequest () 
	{
		splashCircleDrawer.CreatePoints(radius);
	}

	public override void cast(string smName)
	{
		SpellModificator sm = gameObject.GetComponent<SpellCreating> ().getModIfExists (smName);
		splashCircleDrawer.CreatePoints(radius);

		bool waterFound = false;
		Vector3 waterPos = new Vector3();
		Vector3 waterDestination;
		Collider[] intersectObjs = Physics.OverlapSphere(transform.position, radius);
		foreach (var obj in intersectObjs)
		{
			if (obj.tag == "Water")
			{
				waterFound = true;
				waterPos = obj.transform.position + new Vector3(0, 1.5f, 0);
				print("Water for splash casting found");
				break;
			}
		}

		if (waterFound)
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				waterDestination = hit.point;
				Quaternion towaradsPoint = Quaternion.LookRotation(waterDestination - waterPos + new Vector3(0, 0.75f, 0));
				GameObject splash = Instantiate(waterSplash, waterPos, towaradsPoint);
				if (sm != null && sm is RepeatModificator) {
					float wait = ((RepeatModificator)sm).wait;
					Instantiate(waterSplash, waterPos, towaradsPoint);
					StartCoroutine (repeatCast (wait, waterPos, towaradsPoint));
				} else splash.GetComponent<SplashScript>().ApplyModificator(sm);
			}
		}
	}

	private IEnumerator repeatCast (float wait, Vector3 spellSpawnPos, Quaternion rotation) {
		yield return new WaitForSeconds (wait);
		Instantiate(waterSplash, spellSpawnPos, rotation);
	}

	public override string Description {
		get {
			return "splash";
		}
	}


	public override string[] Aliases {
		get {
			return aliases;
		}
	}
}
