using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetSplashScript : NetworkBehaviour
{

    public float speed;
    private float timeLeft;
    private float timeToExpand;
    private List<GameObject> collidesWith = new List<GameObject>();
    public bool leftSplashExists;
    public bool rightSplashExists;
    public int attackPower;

    void OnTriggerEnter(Collider collision)
    {
        if (!collidesWith.Contains(collision.gameObject) && collision.gameObject.name != "NetWaterSplash(Clone)")
        {
            collidesWith.Add(collision.gameObject);
            if (collision.gameObject.CompareTag("Destroyable"))
            {   // Объект, в который врезались, уничтожаемый?
                NetMortal HP = collision.gameObject.GetComponent<NetMortal>();
                HP.lowerHP(attackPower);
            }
            else if (!collision.gameObject.CompareTag("Spell"))
            {
                NetworkServer.Destroy(gameObject);
            }
        }
    }

    

    // Use this for initialization
    void Awake()
    {
        timeLeft = 4.0f;
        timeToExpand = 0.5f;
        leftSplashExists = false;
        rightSplashExists = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        timeLeft -= Time.deltaTime;
        timeToExpand -= Time.deltaTime;
        if (timeLeft < 0)
        {
            NetworkServer.Destroy(gameObject);
        }
        if (timeToExpand < 0)
        {
            if (!leftSplashExists)
            {
                RpcCreateLeft();
            }
            if (!rightSplashExists)
            {
                RpcCreateRight();
            }
        }
    }
    
    private void RpcCreateLeft()
    {
        if (!leftSplashExists)
        {
            GameObject created = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
            created.name = "NetWaterSplash(Clone)";
            created.transform.Translate(Vector3.forward * -0.2f); // Move backwards

            created.transform.Rotate(0, 90, 0);
            created.transform.Translate(Vector3.forward * 0.33f); // Move left
            created.transform.Rotate(0, -90, 0);
            leftSplashExists = true;

            NetSplashScript splController = created.GetComponent<NetSplashScript>();
            splController.CreatedFromRight();
            splController.SetTimeLeft(timeLeft);
        }
    }
    
    private void RpcCreateRight()
    {
        if (!rightSplashExists)
        {
            GameObject created = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
            created.name = "NetWaterSplash(Clone)";
            created.transform.Translate(Vector3.forward * -0.2f); // Move backwards

            created.transform.Rotate(0, -90, 0);
            created.transform.Translate(Vector3.forward * 0.33f);  // Move right
            created.transform.Rotate(0, 90, 0);
            rightSplashExists = true;

            NetSplashScript splController = created.GetComponent<NetSplashScript>();
            splController.CreatedFromLeft();
            splController.SetTimeLeft(timeLeft);
        }
    }

    public void SetTimeLeft(float time)
    {
        timeLeft = time;
    }

    public void CreatedFromLeft()
    {
        leftSplashExists = true;
    }

    public void CreatedFromRight()
    {
        rightSplashExists = true;
    }
}
