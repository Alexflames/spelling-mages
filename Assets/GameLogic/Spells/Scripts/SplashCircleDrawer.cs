using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SplashCircleDrawer : MonoBehaviour {
    // Variables for drawing circle
    [Range(0, 50)]
    public int segments = 50;

    LineRenderer line;
    public float lastingTime = 0.5f;
    float lastingTimeLeft = 0;
    float lastUpdateTime;
    float circleRadius;

    // Use this for initialization
    void Start () {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
    }

    // Draw circle indicating water search radius
    public void CreatePoints(float radius)
    {
        line.enabled = true;
        lastingTimeLeft = lastingTime;
        circleRadius = radius;

        float x;
        //float y;
        float z;
        float angle = 20f;

        UpdatePoints();

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * circleRadius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * circleRadius;

            line.SetPosition(i, new Vector3(x, 0.3f, z));

            angle += (360f / segments);
        }

        
    }

    void UpdatePoints()
    {
        float circleAlpha = lastingTimeLeft / lastingTime;

        line.startColor = new Color(0.1f, 0.8f, 1.0f, 0.7f * circleAlpha);
        line.endColor = new Color(0.1f, 0.8f, 1.0f, 0.7f * circleAlpha);
        
        lastUpdateTime = lastingTimeLeft;
    }

    // Update is called once per frame
    void Update () {

        if (lastingTimeLeft > 0)
        {
            lastingTimeLeft -= Time.deltaTime;

            if (lastUpdateTime - lastingTimeLeft > 0.03)
            {
                print("updated!");
                UpdatePoints();
            }

            //Gradient myGrad = new Gradient();
            //myGrad.alphaKeys = new GradientAlphaKey[1] { new GradientAlphaKey(0.1f, 1) };
            //line.colorGradient = myGrad;
        }
        else
        {
            line.enabled = false;
        }
    }
}
