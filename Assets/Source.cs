using UnityEngine;
using System.Collections;

public class Source : MonoBehaviour {

	protected RaycastHit hit;
	protected Vector3 lightDir;
	protected Vector3 lightPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		Debug.DrawRay (lightPos, lightDir * 10, Color.red);
		if (Physics.Raycast (lightPos, lightDir, out hit)) {
			//Debug.Log("touché");
			hit.transform.gameObject.SendMessage("pointHit", hit.point);
			hit.transform.gameObject.SendMessage("surfaceHit", hit.normal);
			hit.transform.gameObject.SendMessage("Reflect", lightDir);
		}
	}
}
