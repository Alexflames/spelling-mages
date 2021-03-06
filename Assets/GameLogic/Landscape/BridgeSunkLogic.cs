﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSunkLogic : MonoBehaviour {
    private bool bridgeUp = true;
    public bool activated = false;
    private float timeLeft = 1.0f;

    public void Activate () {
        activated = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (activated)
        {
            if (bridgeUp)
            {
                timeLeft -= Time.deltaTime;
                transform.Translate(Vector3.down * 0.06f, Space.World);
                if (timeLeft < 0)
                {
                    activated = false;
                    bridgeUp = false;
                    timeLeft = 1.0f;
                }
            }
            else
            {
                timeLeft -= Time.deltaTime;
                transform.Translate(Vector3.up * 0.06f, Space.World);
                if (timeLeft < 0)
                {
                    activated = false;
                    bridgeUp = true;
                    timeLeft = 1.0f;
                }
            }
        }
	}
}
