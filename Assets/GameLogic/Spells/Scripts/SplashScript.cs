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
    private float attackFactor = 1.0f;
    private float speedFactor = 1.0f;
    private SpellModificator appliedMod = null;

    public void ApplyModificator (SpellModificator sm)
    {
        if (sm == null) return;
        appliedMod = sm;
        if(sm is StrongModificator)
        {
                attackFactor =  ((StrongModificator)sm).factor;
        }
        if(sm is GreatModificator)
        {
		float sF = ((GreatModificator)sm).scaleFactor;
                gameObject.transform.localScale += new Vector3 (sF - 1.0f, 0, sF - 1.0f) ;
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
        if (collision.gameObject.name != "WaterSplash(Clone)")
        {
            if (collision.gameObject.CompareTag("Destroyable"))
            {   // Объект, в который врезались, уничтожаемый?
                Mortal HP = collision.gameObject.GetComponent<Mortal>();
                HP.lowerHP((int)(attackFactor * attackPower));
            }
            else if (!collision.gameObject.CompareTag("Spell"))
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
        transform.Translate(Vector3.forward * (Time.deltaTime * speed * speedFactor));
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
                splController.ApplyModificator (appliedMod);
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
                splController.ApplyModificator (appliedMod);
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
