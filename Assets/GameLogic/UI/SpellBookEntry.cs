using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookEntry : MonoBehaviour {
	//private Text
	public void setText (string text) {
		gameObject.GetComponentInChildren<Text>().text = text;
	}
}
