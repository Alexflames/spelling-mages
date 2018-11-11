using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetBuffDebuffController : NetworkBehaviour
{
    public List<Buff> buffDebuffList;
    List<Buff> toRemove;

    // Use this for initialization
    void Start()
    {
        buffDebuffList = new List<Buff>();
        toRemove = new List<Buff>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Buff buff in buffDebuffList)
        {
            if (!buff.IsActive())
            {
                toRemove.Add(buff);
                continue;
            }
            else if (toRemove.Contains(buff))
            {
                continue;
            }
            else
            {
                buff.Update();
                //print(buff.GetType().Name);
            }
        }
        foreach (Buff buff in toRemove)
        {
            buffDebuffList.Remove(buff);
            //print("Destroy"+buff.GetType().Name);
            buff.Destroy();
        }
        toRemove.Clear();
    }

    public void addBuff(Buff buff)
    {
        buff.Init();
        buffDebuffList.Add(buff);
    }

    public void MarkToRemove(Buff buff)
    {
        if (buffDebuffList.Contains(buff))
        {
            toRemove.Add(buff);
        }
    }

    public List<Buff> getSameBuffs(Type t)
    {
        List<Buff> ans = new List<Buff>();
        foreach (Buff buff in buffDebuffList)
        {
            if (buff.GetType().Equals(t))
            {
                ans.Add(buff);
            }
        }
        return ans;
    }

    public void removeAllBuffs(Type t)
    {
        foreach (Buff buff in buffDebuffList)
        {
            if (buff.GetType().Equals(t))
            {
                toRemove.Add(buff);
            }
        }
    }
}
