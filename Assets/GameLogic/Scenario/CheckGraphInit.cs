using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGraphInit : AbstractSpellInit
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
    public override void cast(string smName)
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

    public override string Description
    {
        get
        {
            return "Проверка, получен ли каркас дерева?";
        }
    }

    public override string[] Aliases {
        get {
            return aliases;
        }
    }
}
