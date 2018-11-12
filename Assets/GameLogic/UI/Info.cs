using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour {
	public GameObject infoPanel;
	private Text header;
	// Use this for initialization
	void Start () {
		int children = infoPanel.transform.childCount;
		for (int i = 0; i < children; ++i){
			Transform child = infoPanel.transform.GetChild (i);
			if (child.name == "Header"){
				header = child.gameObject.GetComponent<Text> ();
				break;
			}
		}
		infoPanel.SetActive (false);
	}
	
	void SetInfo (string name){
		infoPanel.SetActive(true);
		header.text = name;
	}
}
