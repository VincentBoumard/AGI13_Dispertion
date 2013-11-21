using UnityEngine;
using System.Collections;

public class Emission : MonoBehaviour {

	private bool isLit = false;
	private Vector3 lightDir = new Vector3(1.0f, 0.0f, 0.0f);
	private Vector3 lightPos = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 surfaceNormal = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLit) {
			Debug.DrawRay(lightPos, surfaceNormal, Color.blue);
			Debug.DrawRay(lightPos, lightDir, Color.red);
		}
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

	void surfaceHit(Vector3 normal){
		surfaceNormal = normal;
	}

	void pointHit(Vector3 coord){
		lightPos = coord;
	}

	void lightsOff(){
		isLit = false;
	}

	void Reflect(Vector3 lightRay){
		isLit = true;
		float angle = Vector3.Angle (lightRay, surfaceNormal);
		Quaternion rot = Quaternion.AngleAxis (2 * angle, this.transform.up);
		lightDir = -(rot * lightRay);
		//Debug.Log (lightDir);
	}
}
