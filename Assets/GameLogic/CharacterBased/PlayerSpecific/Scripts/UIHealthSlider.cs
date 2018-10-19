using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthSlider : MonoBehaviour {
	public Slider healthSlider;
	public GameObject UIPlayerHp;

	// Use this for initialization
	void Awake () {
		UIPlayerHp = GameObject.FindGameObjectWithTag ("UIPlayerHP");
		healthSlider = UIPlayerHp.GetComponent<Slider> ();
	}

	void Start() {

	}

	public void setMaxHP (int value) {
		healthSlider.maxValue = value;
	}

	public void changeHP (int value) {
		healthSlider.value = value;
	}
}
