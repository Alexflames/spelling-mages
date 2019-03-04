using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIElementsController : MonoBehaviour {

    public GameObject UITypeText;
    public GameObject UISpellBook;
    public GameObject UIModBook;
    public GameObject UIPlayerHP;
    public GameObject UIRMBTimer;
    private Slider UIRMBTimerSlider;
	// Use this for initialization
	void Awake () {
        UIRMBTimerSlider = UIRMBTimer.GetComponent<Slider>();
        UITypeText.SetActive(true);
        UISpellBook.SetActive(true);
        UIPlayerHP.SetActive(true);
        UIModBook.SetActive(true);
	}

    public void SetTimerSliderValue(float value, float maxV)
    {
        UIRMBTimerSlider.maxValue = maxV;
        UIRMBTimerSlider.value = value;
    }

    /// <summary>
    /// Активация/Деактивация слайдера
    /// </summary>
    /// <param name="">true - активировать, false - деактивировать</param>
    public void ActivateRMBTimer(bool option)
    {
        UIRMBTimer.SetActive(option);
    }


	
}
