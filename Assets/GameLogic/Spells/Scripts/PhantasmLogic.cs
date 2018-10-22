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
    private double factor = 1.0;

    public void ApplyModificator (SpellModificator sm)
    {
        if (sm == null) return;
        if(sm is StrongModificator)
        {
                factor =  (((StrongModificator)sm).factor);
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
            HP.lowerHP((int)(factor * attackPower));
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
                rb.AddForce(vectorToOwner * timePassed * 4);
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
            transform.Translate(vectorToOwner * 0.25f, Space.World);
    }

    public void SetOwner(GameObject thatOwns)
    {
        owner = thatOwns;
    }
}
