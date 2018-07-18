using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLeverLogic : MonoBehaviour {
    public GameObject bridgeToActivate;
    public GameObject bridgeToActivate2;
    public GameObject leverPull;
    private BridgeSunkLogic bsl;
    private BridgeSunkLogic bsl2;
    bool leverPingPong = true; // Lever has 2 variations
    private NavMeshChangesScript navMeshChangesScript;
    public float startingTimer = 0.05f;
    float timer;

    void Awake()
    {
        bsl = bridgeToActivate.GetComponent<BridgeSunkLogic>();
        bsl2 = bridgeToActivate2.GetComponent<BridgeSunkLogic>();
        navMeshChangesScript = gameObject.GetComponent<NavMeshChangesScript>();
        timer = startingTimer;
    }

    void OnTriggerEnter(Collider collision)
    {
        print("gooooooooooo!");
        if (collision.gameObject.tag == "Destroyable")
        {
            bsl.Activate();
            bsl2.Activate();
            if (leverPingPong)
            {
                leverPull.transform.Rotate(0, 180.0f, 0, Space.World);
                leverPingPong = false;
            }
            else
            {
                leverPull.transform.Rotate(0, -180.0f, 0, Space.World);
                leverPingPong = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (bsl.activated)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                navMeshChangesScript.RebuildNavMesh();
                timer = startingTimer;
            }
        }
    }
}
