using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotatingShootingDummy : MonoBehaviour
{
    public float diamondPeriodicTime = 0;
    public float timeToCastDiamond = 2.0f;
    System.Random rand = new System.Random();
    private float rotateTime = 0;
    private float rotatePeriod = 1.5f;
    SpellCreating scComp;

    // Use this for initialization
    void Start()
    {
        scComp = gameObject.GetComponent<SpellCreating>();
    }

    void FixedUpdate()
    {
        diamondPeriodicTime += Time.deltaTime;
        rotateTime += Time.deltaTime;
        if(rotateTime > rotatePeriod)
        {
            rotatePeriod = 0.5f + (float)rand.NextDouble();
            gameObject.transform.Rotate(Vector3.up * (float)(rand.NextDouble() * 360.0));
            rotateTime = 0;
        }
        if (diamondPeriodicTime > timeToCastDiamond)
        {
            scComp.castSpell("diamond");
            diamondPeriodicTime = 0;
            timeToCastDiamond = 0.5f + (float)(rand.NextDouble() * 1.5);
        }
    }
}
