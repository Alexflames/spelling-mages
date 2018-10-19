using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitCamera : NetworkBehaviour {
    string _gameObjectName = "NetAIThirdPersonController";

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            GameObject player = transform.Find(_gameObjectName).gameObject;
            var cam = GameObject.Find("Main Camera").GetComponent<CameraAxisBlocker>();
            cam.SetPlayer(player);
            cam.transform.position = player.transform.position + Vector3.up * 10;
        }
	}
}
