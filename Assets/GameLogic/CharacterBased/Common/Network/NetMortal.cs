using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetMortal : NetworkBehaviour
{
    public int startingHealth;
    [SyncVar]
    public int health;

    void Awake()
    {
        if (startingHealth == 0)
        {
            startingHealth = 100;
        }
    }

    void Start()
    {
        health = startingHealth;
    }
    
    public virtual void lowerHP(int value)
    {
        health -= value;
        if (health < 1)
        {
            dies();
        }
    }

    void dies()
    {
        print(gameObject.name + " is ded");
        Destroy(gameObject);
    }

}
