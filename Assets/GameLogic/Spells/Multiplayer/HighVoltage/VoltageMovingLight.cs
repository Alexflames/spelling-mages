using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltageMovingLight : MonoBehaviour {
	Light m_light;

	// Update is called once per frame
	void Awake ()
	{
		m_light = GetComponent<Light>();
	}
	void Update () {
		m_light.range += 0.05f;
		transform.Translate(new Vector3(0, 0, 0.05f));
	}
}
