using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsInit : AbstractSpellInit
{
    GraphController controller;
    private string[] aliases = { "scissors" };
    bool activated = false;

    void OnTriggerEnter(Collider collider)
    {
        if (activated && collider.gameObject.name == "EdgeModel")
        {
            controller.RemoveEdge(collider.gameObject.transform.parent.gameObject);
        }
    }

    void Awake()
    {
        controller = GameObject.Find("GraphController").GetComponent<GraphController>();
    }

    public override void cast(string smName)
    {
        //SpellModificator sm = gameObject.GetComponent<SpellCreating>().getModIfExists(smName);
        activated = !activated;
    }

    public override string Description
    {
        get
        {
            return "Персонаж убирает лишние соединения, прикасаясь к ним";
        }
    }

    public override string[] Aliases {
        get {
            return aliases;
        }
    }
}
