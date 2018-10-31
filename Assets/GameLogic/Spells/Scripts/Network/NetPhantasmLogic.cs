using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPhantasmLogic : NetworkBehaviour
{
    private Rigidbody rb;
    public GameObject owner;
    private bool isghost;
    private float timePassed;
    private Vector3 vectorToOwner;
    public Material activatedPhantasmMaterial;
    private Renderer rend;
    public int attackPower = 25;
    private double attackFactor = 1.0;
    private double speedFactor = 1.0;
    private List<GameObject> collidesWith = new List<GameObject>(); // Whom phantasm already collided with

    public void ApplyModificator(SpellModificator sm)
    {
        if (sm == null) return;
        if (sm is StrongModificator)
        {
            attackFactor = (((StrongModificator)sm).factor);
        }
        if (sm is QuickModificator)
        {
            QuickModificator qm = (QuickModificator)sm;
            attackFactor = 1 / qm.weakFactor;
            speedFactor = qm.speedFactor;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject == owner)  // TODO
        {
            isghost = false;
            rend.material = activatedPhantasmMaterial;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
        else if (isghost)
        {

        }
        else if (collision.gameObject.CompareTag("Destroyable"))
        { // Объект, в который врезались, уничтожаемый?
            if (!isServer) return;

            RpcHit(collision.gameObject);
        }
        else if (collision.gameObject.tag != "Spell")
            Object.Destroy(gameObject);
    }

    [ClientRpc]
    private void RpcHit(GameObject collision)
    {
        if (!collidesWith.Contains(collision.gameObject))
        {
            print(collision.name);
            collidesWith.Add(collision.gameObject);
            NetMortal HP = collision.GetComponent<NetMortal>();
            HP.lowerHP((int)(attackFactor * attackPower));
        }
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
            Vector3 distance = owner.transform.position - gameObject.transform.position + new Vector3(0, 1.0f, 0);
            if (distance.magnitude > 1.5f)
            {
                vectorToOwner = Vector3.Normalize(owner.transform.position - gameObject.transform.position + new Vector3(0, 1.0f, 0));
            }

            if (timePassed < 7.0f)
            {
                rb.AddForce(vectorToOwner * timePassed * 4 * (float)speedFactor);
            }
            else
            {
                if (!rb.IsSleeping())
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.Sleep();
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
