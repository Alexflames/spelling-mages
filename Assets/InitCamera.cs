using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitCamera : NetworkBehaviour {

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            var cam = GameObject.Find("Main Camera").GetComponent<CameraAxisBlocker>();
            cam.SetPlayer(gameObject);
        }
	}
}
