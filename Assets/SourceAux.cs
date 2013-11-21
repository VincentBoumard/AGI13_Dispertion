using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Emission))]

public class SourceAux : Source {

	// Use this for initialization
	void Start () {
		base.lightDir = new Vector3 (0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Emission em = this.GetComponent <Emission> ();
		if (em.getIsLit ()) {
			base.lightDir = em.getLightDir();
			base.lightPos = em.getLightPos();
		}
		base.Update();
	}
}
