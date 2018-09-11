using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AIExponentialDummy : MonoBehaviour
{
    private double diamondPeriodicTime = 0;
    private double timeToCastDiamond = 2.0f;
    public double lambdaParam = 2.0f;
    System.Random rand = new System.Random();
    SpellCreating scComp;

    // Use this for initialization
    void Start()
    {
        scComp = gameObject.GetComponent<SpellCreating>();
    }

    void FixedUpdate()
    {
        diamondPeriodicTime += Time.deltaTime;
        if (diamondPeriodicTime > timeToCastDiamond)
        {
            scComp.castSpell("diamond");
            diamondPeriodicTime = 0;
            timeToCastDiamond = exponential(rand,lambdaParam);
            print(timeToCastDiamond);
        }
    }

    public static double exponential(System.Random rand, double lambda)
    {
        double alpha = rand.NextDouble();
        return Math.Log(1 - alpha) / (-lambda);
    }
}