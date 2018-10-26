using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondLogic : MonoBehaviour {

    public Rigidbody rb;
    public Collider colli;
    int layerMask;
    public int attackPower = 150;
    private double attackFactor = 1.0;
    private double speedFactor = 1.0;

    void Awake () {
        Destroy (this.gameObject, 5);
    }

    void Start () {
        rb = GetComponent<Rigidbody> ();
        colli = GetComponent<Collider> ();
    }
	
	// Update is called once per frame
	//void Update () {

	//}

    public void ApplyModificator (SpellModificator sm)
    {
        if (sm == null) return;
        if(sm is StrongModificator)
        {
                attackFactor =  (((StrongModificator)sm).factor);
        }
        if(sm is QuickModificator)
        {
                QuickModificator qm = (QuickModificator)sm;
                attackFactor = 1 / qm.weakFactor;
                speedFactor = qm.speedFactor;
        }
    }

    void FixedUpdate () {
        rb.AddForce (transform.forward * 1.5f * (float)speedFactor);
        Collider[] intersectObjs = Physics.OverlapSphere (transform.position, 0.25f);
        if (intersectObjs.Length != 0) {
            if (intersectObjs [0].CompareTag ("Destroyable")) { // Объект, в который врезались, уничтожаемый?
                Mortal HP = intersectObjs[0].GetComponent<Mortal>();
                HP.lowerHP ((int)(attackPower * attackFactor));
            }
            if (!intersectObjs[0].gameObject.CompareTag("Spell")) Object.Destroy (gameObject);
        }
    }
}
