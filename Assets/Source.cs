using UnityEngine;
using System.Collections;

public class Source : MonoBehaviour {

	protected RaycastHit hit;
	protected GameObject oldHit = null;
	protected Vector3 lightDir;
	protected Vector3 lightPos;
	private float dist;
	public bool isCreated = false;

	public GameObject lightBeam;
	private GameObject lightb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		// check if the light beam intersects hits an object
		if (Physics.Raycast (lightPos, lightDir, out hit)) {
			if(hit.transform.gameObject.name == "Mirror"){
				// store the game object we're hitting, to send it a message when it's no longer the case
				oldHit = hit.transform.gameObject;
				MirrorScript ms = hit.transform.gameObject.GetComponent<MirrorScript>();
				ms.Reflect(hit.point, hit.normal, lightDir);
				// distance to the object hit to properly create the light beam
				dist = hit.distance;
			} else if(hit.collider == null){
				//not used
			}
		} else {
			dist = 100.0f;
			if(oldHit != null){
				// notify the previous object it's no longer lit, and forget it
				oldHit.GetComponent<MirrorScript>().isLit = false;
				oldHit = null;
			}
		}
		
		if (isCreated) {
			// we need to rotate the light beam each frame so that it's always in the correct direction
			float angle = Vector3.Angle (lightb.transform.forward, lightDir);
			angle = Vector3.Cross (lightb.transform.forward, lightDir).y > 0 ? angle : -angle;
			Quaternion goal = lightb.transform.rotation * Quaternion.AngleAxis(angle, transform.up);
			// use Slerp to interpolate between the old and new position
			lightb.transform.rotation = Quaternion.Slerp(lightb.transform.rotation, goal, Time.deltaTime * 30.0f);
			// set the other parameters right according to the raycast info
			lightb.transform.localScale = new Vector3(0.5f, 1.0f, dist);
			lightb.transform.position = lightPos;
		}
	}

	public void CreateLight(){
		if (!isCreated) {
			isCreated = true;
			// create an instance of the LightBeam prefab with the correct parameters
			lightb = (GameObject)Instantiate (lightBeam);
			lightb.transform.position = this.transform.position;
			if(hit.collider == null) dist = 100.0f;
			lightb.transform.localScale = new Vector3(0.5f, 1.0f, dist);
			Quaternion rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.forward, lightDir), transform.up);
			lightb.transform.rotation = rotation;
		}
	}

	public void DestroyLight(){
		Destroy (lightb);
		isCreated = false;
	}
}
