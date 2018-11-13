using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (isLocalPlayer) {
                spellBook.GetComponent<AddSpellToBook> ().Reset ();
                modBook.GetComponent<AddModToBook> ().Reset ();
            }
        }
        UIHealthScript.changeHP(health);
    }

    void dies()
    {
        print(gameObject.name + " is ded");
        Destroy(gameObject);
    }

}
