using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetFieryAura : NetEmptyAura
{
    NetFieryAuraInit NFAI;
    public override string Name
    {
        get
        {
            return "Fiery";
        }
    }

    void Awake()
    {
        NFAI = gameObject.GetComponent<NetFieryAuraInit>();
    }

    public override GameObject AuraModel { get; set; }

    public override void CastReaction()
    {
        float numberOfShots = 8;
        float angle = 360 / numberOfShots;
        for (int i = 0; i < numberOfShots; i++)
        {
            print("spawned");
            GameObject.Instantiate(NFAI.FieryBurst, 
                AuraModel.transform.position + AuraModel.transform.forward * 3 + Vector3.up, 
                AuraModel.transform.rotation);
            AuraModel.transform.Rotate(new Vector3(0, AuraModel.transform.position.y), angle);
        }
    }
}
