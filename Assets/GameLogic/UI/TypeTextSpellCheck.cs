using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypeTextSpellCheck: MonoBehaviour {

	private Text text;
	private Color originalColor, alertColor;

	public void Start () {
		text = this.gameObject.GetComponent<Text>();
		originalColor = text.color;
		alertColor = Color.red;
	}
	
	public void Alert () {
		text.color = alertColor;
	}

	public void Unalert () {
		text.color = originalColor;
	}
}
