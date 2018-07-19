using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAxisBlocker : MonoBehaviour {
	public float axisY;
	public Vector3 position;
	public GameObject player;
	private Vector3 playerInitialPosition;

	// Use this for initialization
	void Start () {
		axisY = gameObject.transform.position.y;
		position = gameObject.transform.position;
		playerInitialPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
			gameObject.transform.position = position +
				new Vector3 (player.transform.position.x, 0, player.transform.position.z) -
				playerInitialPosition;
		}
	}
}
