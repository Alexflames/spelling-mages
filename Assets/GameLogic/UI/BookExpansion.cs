using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookExpansion : MonoBehaviour {
	public GameObject spellBook, modBook;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftControl)){
			 modBook.SetActive(!modBook.activeSelf);
		}
		if (Input.GetKeyDown(KeyCode.Tab)){
			 spellBook.SetActive(!spellBook.activeSelf);
		}
	}
}
