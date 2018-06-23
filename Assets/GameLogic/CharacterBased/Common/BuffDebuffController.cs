using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffController : MonoBehaviour {
    public class BuffDebuff
    {
        public float TimeLeft { get; set; }
        public float LastTick { get; set; }
        public int Amount { get; set; }
        public BuffDebuff(float time)
        {
            TimeLeft = time;
            LastTick = time;
            Amount = 1;
        }
    }

    public Dictionary<string, BuffDebuff> buffDebuffMap;
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPersonMovement;
    List<String> toRemove; // Keys in dictionary(buff names) of ones that will be removed

    // Use this for initialization
    void Start () {
        thirdPersonMovement = gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        buffDebuffMap = new Dictionary<string, BuffDebuff>();
        toRemove = new List<string>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        foreach (var buff in buffDebuffMap)
        {
            buff.Value.TimeLeft = Math.Max(0, buff.Value.TimeLeft - Time.deltaTime);
            switch (buff.Key)
            {
                case "water-slow":
                    if (buff.Value.TimeLeft <= 0)
                    {
                        print("Not slow anymore!");
                        thirdPersonMovement.ChangeSpeed(1 / 0.7f);
                        toRemove.Add(buff.Key);
                    }
                    break;
                default:
                    break;
            }
        }
        foreach (String key in toRemove)
        {
            buffDebuffMap.Remove(key);
        }
        toRemove.Clear();
	}
    
    public void updateBuff(string name, float time)
    {
        switch (name)
        {
            case "water-slow":
                if (buffDebuffMap.ContainsKey(name))
                {
                    buffDebuffMap[name].TimeLeft = time;
                }
                else
                {
                    buffDebuffMap[name] = new BuffDebuff(time);
                    thirdPersonMovement.ChangeSpeed(0.7f);
                }
                break;
            default:
                print("BUFF " + name + " NOT FOUND");
                break;
        }
    }
}
