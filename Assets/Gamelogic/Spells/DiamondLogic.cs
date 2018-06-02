using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondLogic : MonoBehaviour {

	public Rigidbody rb;
	public Collider colli;
	int layerMask;
	int attackPower;

	void Awake () {
		Destroy (this.gameObject, 5);
	}

	void Start () {
		rb = GetComponent<Rigidbody> ();
		colli = GetComponent<Collider> ();
		attackPower = 150;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		rb.AddForce (transform.forward * 1.5f);

		Collider[] intersectObjs = Physics.OverlapSphere (transform.position, 0.2f);
		if (intersectObjs.Length != 0) {
			if (intersectObjs [0].CompareTag ("Destroyable")) { // Объект, в который врезались, уничтожаем?
				Mortal HP = intersectObjs[0].GetComponent<Mortal>();
				HP.lowerHP (attackPower);
				DestroyObject (gameObject);
			}
		}
	}
}
