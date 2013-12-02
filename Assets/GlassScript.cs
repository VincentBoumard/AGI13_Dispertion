using UnityEngine;
using System.Collections;

public class GlassScript : MonoBehaviour
{

    public bool isLit = false;
    private Vector3[] lightDir = new Vector3[4];//(1.0f, 0.0f, 0.0f);
    private Vector3 lightPos = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 surfaceNormal = new Vector3(0.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            lightDir[i] = new Vector3(1.0f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLit)
        {
            // used for debug purposes
        }
        else
        {
            Source src = this.gameObject.GetComponent<Source>();
            if (src.isCreatedD)
                src.DestroyDispersedLight();
        }
        // test function to rotate the mirrors in-game
        if (Input.GetKeyDown("b"))
            transform.Rotate(0.0f, 5.0f, 0.0f);
    }

    public bool getIsLit()
    {
        return isLit;
    }

    public Vector3[] getLightDir()
    {
        return lightDir;
    }

    public Vector3 getLightPos()
    {
        return lightPos;
    }

    void lightsOff()
    {
        isLit = false;
    }

    public void Dispers(Vector3 coord, Vector3 normal, Vector3 lightRay)
    {
        isLit = true;
        surfaceNormal = normal;
        lightPos = coord;
        Debug.Log("LightPos of dispersion" + lightPos);
        for(int i = 0; i < 4; i++)
        {
            float angle = Vector3.Angle(lightRay, surfaceNormal);
            // Vector3.Angle gives an absolute value, so we have to check if the reflected ray should be
            // reflected to the right or to the left
            angle = Vector3.Cross(lightRay, surfaceNormal).y > 0 ? angle : -angle;
            Quaternion rot = Quaternion.AngleAxis(2*angle, this.transform.up);
            lightDir[i] = (rot * lightRay)+new Vector3(i, 0, 0);
        }
    }
}
