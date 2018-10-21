using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetSpellCreating : NetworkBehaviour
{
    public SpellCreating spellCreating;

    void Start ()
    {
        if (isLocalPlayer)
        {
            spellCreating = gameObject.AddComponent<SpellCreating>();
            spellCreating.randomise = true;
            spellCreating.spellBookText = GameObject.Find("SpellBook").GetComponent<Text>();
        }
    }
}
