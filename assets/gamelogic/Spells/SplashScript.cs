using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour {

    public float speed;
    private float timeLeft;
    private float timeToExpand;
    private GameObject[] collidesWith;
    public bool leftSplashExists;
    public bool rightSplashExists;
    public int attackPower;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name != "WaterSplash(Clone)")
        {
            if (collision.gameObject.CompareTag("Destroyable"))
            {   // Объект, в который врезались, уничтожаемый?
                Mortal HP = collision.gameObject.GetComponent<Mortal>();
                HP.lowerHP(attackPower);
            }
            else
            {
                Object.Destroy(gameObject);
            }
        }    
    }

	// Use this for initialization
	void Awake () {
        timeLeft = 4.0f;
        timeToExpand = 0.5f;
        leftSplashExists = false;
        rightSplashExists = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        timeLeft -= Time.deltaTime;
        timeToExpand -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(gameObject);
        }
        if (timeToExpand < 0)
        {
            if (!leftSplashExists)
            {
                GameObject created = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
                created.name = "WaterSplash(Clone)";
                created.transform.Translate(Vector3.forward * -0.2f); // Move backwards

                created.transform.Rotate(0, 90, 0);
                created.transform.Translate(Vector3.forward * 0.33f); // Move left
                created.transform.Rotate(0, -90, 0);
                leftSplashExists = true;

                SplashScript splController = created.GetComponent<SplashScript>();
                splController.CreatedFromRight();
                splController.SetTimeLeft(timeLeft);
            }
            if (!rightSplashExists)
            {
                GameObject created = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
                created.name = "WaterSplash(Clone)";
                created.transform.Translate(Vector3.forward * -0.2f); // Move backwards

                created.transform.Rotate(0, -90, 0);
                created.transform.Translate(Vector3.forward * 0.33f);  // Move right
                created.transform.Rotate(0, 90, 0);
                rightSplashExists = true;

                SplashScript splController = created.GetComponent<SplashScript>();
                splController.CreatedFromLeft();
                splController.SetTimeLeft(timeLeft);
            }
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
