using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetUIHealthSlider))]
public class NetPlayerMortal : NetMortal
{
    private NetUIHealthSlider UIHealthScript;
    private GameObject spellBook, modBook; //to clean them after death

    // Use this for initialization
    void Start()
    {
        UIHealthScript = gameObject.GetComponent<NetUIHealthSlider>();
        setStartingHealth(startingHealth);
        if (isLocalPlayer) {
            spellBook = GameObject.Find ("NewSpellBook");
            modBook = GameObject.Find ("ModBook");
        }
    }

    public void setStartingHealth(int value)
    {
        health = value;
        UIHealthScript.setMaxHP(value);
        UIHealthScript.changeHP(value);
    }

    public override void lowerHP(int value)
    {
        health -= value;
        if (health < 1)
        {
            UIHealthScript.changeHP(0);
            dies();
        }
        UIHealthScript.changeHP(health);
    }

    [Command]
    void CmdTriggerSpawn()
    {
        RpcRespawn();
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            var spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
        setStartingHealth(100);
    }

    void dies()
    {
        if (isLocalPlayer)
        {
            spellBook.GetComponent<SpellBookLogic>().Reset();
            modBook.GetComponent<ModBookLogic>().Reset();
        }
        print(gameObject.name + " is ded");

        NetworkServer.Destroy(gameObject);
    }

}
