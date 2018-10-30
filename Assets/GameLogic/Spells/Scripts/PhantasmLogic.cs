using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantasmLogic : MonoBehaviour {

    private Rigidbody rb;
    private GameObject owner;
    private bool isghost;
    private float timePassed;
    private Vector3 vectorToOwner;
    public Material activatedPhantasmMaterial;
    private Renderer rend;
    public int attackPower;
    private double attackFactor = 1.0;
    private double speedFactor = 1.0;
    private float timeToDestroy = 10.0f;

    public void ApplyModificator (SpellModificator sm)
    {
        if (sm == null) return;
        if(sm is StrongModificator)
        {
                attackFactor =  (((StrongModificator)sm).factor);
        }
        if(sm is GreatModificator)
        {
		float sF = (float)(((GreatModificator)sm).scaleFactor);
                gameObject.transform.localScale += new Vector3 (sF - 1.0f, sF - 1.0f, sF - 1.0f) ;
        }
        if(sm is QuickModificator)
        {
                QuickModificator qm = (QuickModificator)sm;
                attackFactor = 1 / qm.weakFactor;
                speedFactor = qm.speedFactor;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        print(collision.name);
        if (collision.gameObject == owner)  // TODO
        {
            isghost = false;
            rend.material = activatedPhantasmMaterial;
        }
        else if (isghost)
        {

        }
        else if (collision.gameObject.CompareTag("Destroyable"))
        { // Объект, в который врезались, уничтожаемый?
            Mortal HP = collision.GetComponent<Mortal>();
            HP.lowerHP((int)(attackFactor * attackPower));
        }
        else if (collision.gameObject.tag != "Spell")
            Object.Destroy(gameObject);
    }

    void Awake()
    {
        isghost = true;
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        timePassed = 0;
    }

    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        if (isghost)
        {
            vectorToOwner = Vector3.Normalize(owner.transform.position - gameObject.transform.position + new Vector3(0, 1.0f, 0));
            if (timePassed < 7.0f)
                rb.AddForce(vectorToOwner * timePassed * 4 * (float)speedFactor);
            else
            {
                if (!rb.isKinematic)
                {
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }
                transform.Translate(vectorToOwner * 0.25f, Space.World);
            }
        }
        else
        {
            transform.Translate(vectorToOwner * 0.25f, Space.World);
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy < 0.0f) Object.Destroy(gameObject);
        }
    }

    public void SetOwner(GameObject thatOwns)
    {
        owner = thatOwns;
    }
}
