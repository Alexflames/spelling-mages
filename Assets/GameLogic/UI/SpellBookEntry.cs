using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookEntry : MonoBehaviour {
	private Image im;
	
	public void setText (string text) {
		gameObject.GetComponentInChildren<Text>().text = text;
		
	}

	public void setSprite (string sprite) {
		if (im == null) {
			int children = gameObject.transform.childCount;
			for (int i = 0; i < children; ++i){
				Transform child =  gameObject.transform.GetChild (i);
				if (child.name == "Image"){
					im = child.gameObject.GetComponent<Image> ();
				}
			}
		}
		Sprite s = Resources.Load<Sprite> (sprite);
		if (s != null) im.sprite = s;
		else print(s + " sprite not found");
	}
}
