using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetHighVoltageLogic : NetworkBehaviour
{
	float timeToAppear;
	float timeToFade;
	public int attackPower = 50;
	private double attackFactor = 1.0;
	private double speedFactor = 1.0;

	private GameObject owner;
	//--------------ALTERNATIVE
	float defaultTTA;
	// Lightning arcs' generation
	int RNGFREQUENCY = 2;
	public int Nobjects = 4;
	public GameObject SubVoltageObject;
	private List<GameObject> toContinue;
	private List<GameObject> nextToContinue;
	//---------------------------------------//
	//private bool greatMod = false;
	private float sF = 1.0f;//scale factor for great mod

	public void ApplyModificator(SpellModificator sm)
	{
		if (sm == null) return;
		if (sm is NetStrongModificator)
		{
			attackFactor = ((NetStrongModificator)sm).factor;
		}
		if (sm is NetGreatModificator)
		{
			//greatMod = true;
			sF = ((NetGreatModificator)sm).scaleFactor; // TODO: fix to double (non-Net exists)
			gameObject.transform.localScale += new Vector3(sF - 1.0f, 0, sF - 1.0f);
		}
		if (sm is NetQuickModificator)
		{
			NetQuickModificator qm = (NetQuickModificator)sm;
			attackFactor = 1 / qm.weakFactor;
			speedFactor = qm.speedFactor;
		}
	}

	private List<GameObject> collidedWith = new List<GameObject>();

	void OnTriggerEnter(Collider collision)
	{
		if (!collidedWith.Contains(collision.gameObject))
		{
			collidedWith.Add(collision.gameObject);
			if (collision.gameObject.CompareTag("Destroyable"))
			{   // Объект, в который врезались, уничтожаемый?
				if (collision.gameObject != owner)
				{
					NetMortal HP = collision.gameObject.GetComponent<NetMortal>();
					HP.lowerHP((int)(attackPower * attackFactor));
				}
			}
		}
	}

	// Use this for initialization
	void Awake()
	{
		timeToFade = 0.55f;
		defaultTTA = timeToFade / Nobjects - 0.01f;
		timeToAppear = defaultTTA;

		// prepare lists
		toContinue = new List<GameObject>();
		nextToContinue = new List<GameObject>();
		toContinue.Add(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		timeToFade -= Time.deltaTime * (float)speedFactor;
		timeToAppear -= Time.deltaTime * (float)speedFactor;
		// if it is time to spawn next arcs
		if (timeToAppear < 0)
		{
			// For the less-chaotic appearance of lighting skip some of arcs creation on level 4 and deeper (level 1 = first arc)
			float toSkipStart = toContinue.Count <= 1 ? 0 : toContinue.Count / RNGFREQUENCY;
			float toSkip = toSkipStart;
			//
			foreach(var obj in toContinue)
			{
				bool thereIsWall = Physics.Raycast(obj.transform.position, obj.transform.position + obj.transform.forward, 2, LayerMask.GetMask("Default"));
				// try to skip right arc
				if (thereIsWall)
				{
					toSkip -= 2;
				}
				else if (toSkip > 0 && Random.Range(0, toContinue.Count / toSkipStart) < 1)
				{
					toSkip--;
					var arc = Instantiate(SubVoltageObject, obj.transform.position + obj.transform.forward - obj.transform.right * 0.25f, obj.transform.rotation, obj.transform.transform);
					arc.transform.Rotate(new Vector3(0, Random.Range(-25, 25), 0));
					nextToContinue.Add(arc);
				}
				else
				{
					// try to skip left arc
					if (toSkip > 0 && Random.Range(0, toContinue.Count / toSkipStart) < 1)
					{
						toSkip--;
					}
					else
					{
						var arc = Instantiate(SubVoltageObject, obj.transform.position + obj.transform.forward - obj.transform.right * 0.25f, obj.transform.rotation, obj.transform.transform);
						arc.transform.Rotate(new Vector3(0, Random.Range(-25, 25), 0));
						nextToContinue.Add(arc);
					}
					var Rarc = Instantiate(SubVoltageObject, obj.transform.position + obj.transform.forward + obj.transform.right * 0.25f, obj.transform.rotation, obj.transform.transform);
					Rarc.transform.Rotate(new Vector3(0, Random.Range(-25, 25), 0));
					nextToContinue.Add(Rarc);
				}
			}
			toContinue.Clear(); // prepare list of next spawning arcs
			foreach(var obj in nextToContinue)
			{
				toContinue.Add(obj);
			}
			nextToContinue.Clear();
			timeToAppear = defaultTTA;
		}
		else if (timeToFade < 0)
		{
			NetworkServer.Destroy(gameObject);
		}
	}

	public void SetOwner(GameObject thatOwns)
	{
		owner = thatOwns;
	}
}
