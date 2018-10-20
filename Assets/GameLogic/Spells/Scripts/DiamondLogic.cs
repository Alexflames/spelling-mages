using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondLogic : MonoBehaviour {

	public Rigidbody rb;
	public Collider colli;
	int layerMask;
	public int attackPower = 150;
    private double factor = 1.0;

	void Awake () {
		Destroy (this.gameObject, 5);
	}

	void Start () {
		rb = GetComponent<Rigidbody> ();
		colli = GetComponent<Collider> ();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SetFactor (double fact)
    {
        factor = fact;
    }

	void FixedUpdate () {
		rb.AddForce (transform.forward * 1.5f);
        Collider[] intersectObjs = Physics.OverlapSphere (transform.position, 0.25f);
		if (intersectObjs.Length != 0) {
			if (intersectObjs [0].CompareTag ("Destroyable")) { // Объект, в который врезались, уничтожаемый?
				Mortal HP = intersectObjs[0].GetComponent<Mortal>();
                HP.lowerHP ((int)(attackPower * factor));
			}
            if (!intersectObjs[0].gameObject.CompareTag("Spell"))
                Object.Destroy (gameObject);
		}
	}
}
