using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetAIExponentialDummy : NetworkBehaviour
{
    private double diamondPeriodicTime = 0;
    private double timeToCastDiamond = 2.0f;
    public double lambdaParam = 2.0f;
    Random rand;
    NetSpellCreating scComp;

    // Use this for initialization
    void Start()
    {
        scComp = gameObject.GetComponent<NetSpellCreating>();
        rand = new Random();
    }

    void FixedUpdate()
    {
        diamondPeriodicTime += Time.deltaTime;
        if (diamondPeriodicTime > timeToCastDiamond)
        {
            scComp.castSpell("diamond");
            diamondPeriodicTime = 0;
            timeToCastDiamond = exponential(rand, lambdaParam);
        }
    }

    public static double exponential(Random rand, double lambda)
    {
        float alpha = Random.value;
        return System.Math.Log(1 - alpha) / (-lambda);
    }
}