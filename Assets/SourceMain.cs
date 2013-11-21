using UnityEngine;
using System.Collections;

public class SourceMain : Source {

	// Use this for initialization
	void Start () {
		base.lightDir = this.transform.forward;
		base.lightPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
