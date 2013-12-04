using UnityEngine;
using System.Collections;

public class MirrorScript : MonoBehaviour {

	public bool isLit = false;
	private Vector3 lightDir = new Vector3(1.0f, 0.0f, 0.0f);
	private Vector3 lightPos = new Vector3(0.0f, 0.0f, 0.0f);
    private int lightType = 4;
	private Vector3 surfaceNormal = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLit) {
			// used for debug purposes
		} else {
			Source src = this.gameObject.GetComponent<Source>();
			if(src.isCreated)
				src.DestroyLight();
		}
		// test function to rotate the mirrors in-game
		if (Input.GetKeyDown ("v"))
						transform.Rotate (0.0f, 5.0f, 0.0f);
	}

	public bool getIsLit(){
		return isLit;
	}

	public Vector3 getLightDir(){
		return lightDir;
	}

	public Vector3 getLightPos(){
		return lightPos;
	}

    public int getLightType()
    {
        return lightType;
    }
	
	void lightsOff(){
		isLit = false;
	}

	public void Reflect(Vector3 coord, Vector3 normal, Vector3 lightRay, int type){
		isLit = true;
        lightType = type;
		surfaceNormal = normal;
		lightPos = coord;
        Debug.Log("LightPos of reflection" + lightPos);
		float angle = Vector3.Angle (lightRay, surfaceNormal);
		// Vector3.Angle gives an absolute value, so we have to check if the reflected ray should be
		// reflected to the right or to the left
		angle = Vector3.Cross (lightRay, surfaceNormal).y > 0 ? angle : -angle;
		Quaternion rot = Quaternion.AngleAxis (2 * angle, this.transform.up);
		lightDir = -(rot * lightRay);
	}
}
