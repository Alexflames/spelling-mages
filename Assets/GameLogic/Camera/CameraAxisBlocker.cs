﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAxisBlocker : MonoBehaviour {
	public float axisY;
	public Vector3 position;
	public GameObject player;
	private Vector3 playerInitialPosition;
	
    void Awake () {
        if (player)
        {
            SetPlayer(player);
        }
    }
	// Update is called once per frame
	void Update () {
		if (player) {
			gameObject.transform.position = position +
				new Vector3 (player.transform.position.x, 0, player.transform.position.z) -
				playerInitialPosition;
		}
	}

    public void SetPlayer(GameObject pl)
    {
        player = pl;

        axisY = gameObject.transform.position.y;
        position = pl.transform.position + Vector3.up * 18 + Vector3.back * 6;
        if (player)
        {
            playerInitialPosition = player.transform.position;
        }
    }
}
