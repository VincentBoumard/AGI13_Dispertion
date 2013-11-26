﻿using UnityEngine;
using System.Collections;

public class LightBeamScript : MonoBehaviour {

	public float dist;
	public Vector3 forward;

	// Use this for initialization
	void Start () {
		Transform shadow = this.transform.FindChild ("Shadow");
		//Vector3 forward = this.transform.FindChild("Plane").transform.forward;
		//shadow.position = Vector3.Normalize(transform.forward) * dist + new Vector3(0.0f, 2.8f, 0.0f);// - Vector3.Normalize(forward) * 0.5f
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, transform.forward, out hit))
						dist = hit.distance;
		shadow.gameObject.transform.position += transform.forward * (dist - 3.7f);//Vector3.Normalize(forward) * dist + new Vector3(0.0f, 2.8f, 0.0f);
		Debug.Log (dist);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
