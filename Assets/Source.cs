using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Source : MonoBehaviour {

	protected RaycastHit hit;
	protected GameObject oldHit = null;
    protected GameObject[] oldHitD = new GameObject[4];
    protected int hitIndex = -1;
    protected int[] hitIndexD = new int[4];
	protected Vector3 lightDir;
    public int lightType = 5;
    protected Vector3[] lightDirD = new Vector3[4];
	protected Vector3 lightPos;
	private float dist;
    private float[] distD = new float[4];
	public bool isCreated = false;
    public bool isCreatedD = false;

	public GameObject lightBeam;
    public GameObject[] dispersedLightBeams = new GameObject[4];
    private GameObject lightb;
    private GameObject[] lightbD = new GameObject[4];


	// Use this for initialization
	void Start () {        
	
	}
	
	// Update is called once per frame
	protected void Update () {
		// check if the light beam intersects hits an object
        if (isCreated && Physics.Raycast(lightPos, lightDir, out hit))
        {
            if (hit.transform.gameObject.name == "Mirror")
            {
                // store the game object we're hitting, to send it a message when it's no longer the case
                oldHit = hit.transform.gameObject;
                hitIndex = 1;
                MirrorScript ms = hit.transform.gameObject.GetComponent<MirrorScript>();
                ms.Reflect(hit.point, hit.normal, lightDir, lightType);
                // distance to the object hit to properly create the light beam
                dist = hit.distance;
            }

            else if (hit.transform.gameObject.name == "Glass")
            {
                // store the game object we're hitting, to send it a message when it's no longer the case
                oldHit = hit.transform.gameObject;
                hitIndex = 2;
                GlassScript ms = hit.transform.gameObject.GetComponent<GlassScript>();
                ms.Dispers(hit.point, hit.normal, lightDir);
                // distance to the object hit to properly create the light beam
                dist = hit.distance;
            }

            else if (hit.transform.gameObject.name == "Target")
            {
                // store the game object we're hitting, to send it a message when it's no longer the case
                oldHit = hit.transform.gameObject;
                hitIndex = 0;
                Target ms = hit.transform.gameObject.GetComponent<Target>();
                if (ms.getColour() == lightType)
                {
                    ms.Hit();
                    //ms.GetComponent<ParticleSystem>().Play();
                }
                // distance to the object hit to properly create the light beam
                dist = hit.distance;
            }
            else if (hit.collider == null)
            {
                //not used
            }
        }
        else
        {
            dist = 100.0f;
            if (oldHit != null)
            {
                // notify the previous object it's no longer lit, and forget it
                if (hitIndex == 1 && oldHit.GetComponent<MirrorScript>() != null)
                {
                    oldHit.GetComponent<MirrorScript>().isLit = false;

                }
                else if (hitIndex == 1 && oldHit.GetComponent<GlassScript>() != null)
                {
                    oldHit.GetComponent<GlassScript>().isLitOne = false;
                    oldHit.GetComponent<GlassScript>().isLit = false;

                }
                else if (hitIndex == 0 && oldHit.GetComponent<Target>() != null)
                {
                    oldHit.GetComponent<Target>().isHit = false;

                }
                oldHit = null;
            }
        }
        if (isCreatedD)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Physics.Raycast(lightPos, lightDirD[i], out hit))
                {
                    if (hit.transform.gameObject.name == "Mirror")
                    {
                        // store the game object we're hitting, to send it a message when it's no longer the case
                        oldHit = hit.transform.gameObject;
                        hitIndexD[i] = 1;
                        lightType = i;
                        MirrorScript ms = hit.transform.gameObject.GetComponent<MirrorScript>();
                        ms.Reflect(hit.point, hit.normal, lightDirD[i], i);
                        // distance to the object hit to properly create the light beam
                        distD[i] = hit.distance;
                    }

                    if (hit.transform.gameObject.name == "Glass")
                    {
                        // store the game object we're hitting, to send it a message when it's no longer the case
                        oldHit = hit.transform.gameObject;
                        hitIndexD[i] = 2;
                        lightType = i;
                        GlassScript ms = hit.transform.gameObject.GetComponent<GlassScript>();
                        ms.DispersOne(hit.point, hit.normal, lightDirD[i], i);
                        // distance to the object hit to properly create the light beam
                        distD[i] = hit.distance;
                    }
                    else if (hit.transform.gameObject.name == "Target")
                    {
                        // store the game object we're hitting, to send it a message when it's no longer the case
                        oldHit = hit.transform.gameObject;
                        hitIndexD[i] = 0;
                        lightType = i;
                        Target ms = hit.transform.gameObject.GetComponent<Target>();
                        if (i == ms.getColour())
                        {
                            ms.Hit();
                        }
                        // distance to the object hit to properly create the light beam
                        distD[i] = hit.distance;
                    }
                    else if (hit.collider == null)
                    {
                        //not used
                    }
                }
            }
        }
        
        else
        {

            for (int i = 0; i < 4; i++)
            {
                distD[i] = 100.0f;
                if (oldHitD[i] != null)
                {
                    // notify the previous object it's no longer lit, and forget it
                    if (hitIndexD[i] == 1 && oldHitD[i].GetComponent<MirrorScript>() != null)
                    {
                        oldHitD[i].GetComponent<MirrorScript>().isLit = false;

                    }
                    else if (hitIndexD[i] == 2 && oldHitD[i].GetComponent<GlassScript>() != null)
                    {
                        oldHitD[i].GetComponent<GlassScript>().isLit = false;

                    }
                    else if (hitIndexD[i] == 0 && oldHitD[i].GetComponent<Target>() != null)
                    {
                        oldHitD[i].GetComponent<Target>().isHit = false;

                    }
                    oldHitD[i] = null;
                }
            }
        }
    
		
		if (isCreated) {

            float angle = Vector3.Angle(lightb.transform.forward, lightDir);
            angle = Vector3.Cross(lightb.transform.forward, lightDir).y > 0 ? angle : -angle;
            Quaternion goal = lightb.transform.rotation * Quaternion.AngleAxis(angle, transform.up);
            // use Slerp to interpolate between the old and new position
            lightb.transform.rotation = Quaternion.Slerp(lightb.transform.rotation, goal, Time.deltaTime * 30.0f);
            // set the other parameters right according to the raycast info
            lightb.transform.localScale = new Vector3(0.5f, 1.0f, dist);
            lightb.transform.position = lightPos;
            lightb.GetComponent<LightBeamScript>().dist = dist;

		}

        if (isCreatedD)
        {
            for (int i = 0; i < 4; i++ )
            {

                float angle = Vector3.Angle(lightbD[i].transform.forward, lightDirD[i]);
                angle = Vector3.Cross(lightbD[i].transform.forward, lightDirD[i]).y > 0 ? angle : -angle;
                Quaternion goal = lightbD[i].transform.rotation * Quaternion.AngleAxis(angle, transform.up);
                // use Slerp to interpolate between the old and new position
                lightbD[i].transform.rotation = Quaternion.Slerp(lightbD[i].transform.rotation, goal, Time.deltaTime * 30.0f);
                /*lightbD[i].transform.rotation = new Quaternion(lightbD[i].transform.rotation.x, lightbD[i].transform.rotation.y,
                lightbD[i].transform.rotation.z, lightbD[i].transform.rotation.w);*/
                // set the other parameters right according to the raycast info
                lightbD[i].transform.localScale = new Vector3(0.5f, 1.0f, distD[i]);
                lightbD[i].transform.position = lightPos;
                lightbD[i].GetComponent<LightBeamScript>().dist = distD[i];
            }

        }
	}

	public void CreateLight(){
                
        if (!isCreated)
        {
            isCreated = true;
            // create an instance of the LightBeam prefab with the correct parameters
            if (lightType == 5)
            {
                lightb = (GameObject)Instantiate(lightBeam);
                lightb.transform.position = this.transform.position;
                if (hit.collider == null) dist = 100.0f;
                lightb.transform.localScale = new Vector3(0.5f, 1.0f, dist);
                Quaternion rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.forward, lightDir), transform.up);
                lightb.transform.rotation = rotation;

                lightb.GetComponent<LightBeamScript>().forward = lightDir;
            }
            else
            {
                lightb = (GameObject)Instantiate(dispersedLightBeams[lightType]);
                lightb.transform.position = this.transform.position;
                if (hit.collider == null) dist = 100.0f;
                lightb.transform.localScale = new Vector3(0.5f, 1.0f, dist);
                Quaternion rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.forward, lightDir), transform.up);
                lightb.transform.rotation = rotation;

                lightb.GetComponent<LightBeamScript>().forward = lightDir;
            }
        }
	}

    public void CreateDispersedLight()
    {

        if (!isCreatedD)
        {
            isCreatedD = true;
            for (int i = 0; i < 4; i++)
            {
                // create an instance of the LightBeam prefab with the correct parameters
                lightbD[i] = (GameObject)Instantiate(dispersedLightBeams[i]);
                lightbD[i].transform.position = this.transform.position;
                if (hit.collider == null) distD[i] = 100.0f;
                lightbD[i].transform.localScale = new Vector3(0.5f, 1.0f, distD[i]);
                Quaternion rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.forward, lightDirD[i]), transform.up);
                lightbD[i].transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);

                lightbD[i].GetComponent<LightBeamScript>().forward = lightDirD[i];
            }
        }
    }


	public void DestroyLight(){

        Destroy(lightb);
		isCreated = false;
	}

    public void DestroyDispersedLight()
    {
        foreach (GameObject dlb in lightbD)
        {
            Destroy(dlb);
        }
        isCreatedD = false;
    }
}
