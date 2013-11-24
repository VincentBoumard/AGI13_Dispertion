using UnityEngine;
using System.Collections;

public class SourceMain : Source {

	// Use this for initialization
	void Start () {
		base.lightDir = transform.forward;
		base.lightPos = transform.position;
		base.Update ();
		base.CreateLight ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}
}
