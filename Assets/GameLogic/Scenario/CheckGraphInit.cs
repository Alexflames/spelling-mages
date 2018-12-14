using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGraphInit : MonoBehaviour, SpellInit
{
    GraphController controller;
    TextMesh resultText;
    private string[] aliases = { "check answer" };
    float timer = 3;

    void Awake()
    {
        controller = GameObject.Find("GraphController").GetComponent<GraphController>();
        resultText = GameObject.Find("ResultText").GetComponent<TextMesh>();
    }

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<SpellCreating>().addSpell(aliases, this);
    }

    public void cast(string smName)
    {
        //SpellModificator sm = gameObject.GetComponent<SpellCreating>().getModIfExists(smName);
        if (controller.CheckGraph())
        {
            resultText.text = "<color=#99FF99ff>Правильно!</color>";
        }
        else
        {
            resultText.text = "<color=#FF5555ff>Неверно!</color>";
        }
        timer = 3;
    }

    void Update()
    {
        if (resultText.text != "")
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                resultText.text = "";
            }
        }
    }

    public string Description
    {
        get
        {
            return "Проверка, получен ли каркас дерева?";
        }
    }
}
