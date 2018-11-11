using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookEntry : MonoBehaviour {

	public void setText (string text) {
		gameObject.GetComponentInChildren<Text>().text = text;
	}
}
