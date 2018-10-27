using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetUIHealthSlider : NetworkBehaviour
{
    public Slider healthSlider;
    public GameObject UIPlayerHp;

    // Use this for initialization
    void Awake()
    {
        UIPlayerHp = GameObject.FindGameObjectWithTag("UIPlayerHP");
        healthSlider = UIPlayerHp.GetComponent<Slider>();
    }

    public void setMaxHP(int value)
    {
        if (isLocalPlayer)
        {
            healthSlider.maxValue = value;
        }
    }

    public void changeHP(int value)
    {
        if (isLocalPlayer)
        {
            healthSlider.value = value;
        }
    }
}
