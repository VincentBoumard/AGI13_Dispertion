using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MirrorScript))]

public class SourceAux : Source {

	// Use this for initialization
	void Start () {
		base.lightDir = new Vector3 (0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	new void Update () {
		MirrorScript em = this.GetComponent <MirrorScript> ();

        if (em.getIsLit())
        {            
			base.lightDir = em.getLightDir();
			base.lightPos = em.getLightPos();
            base.lightType = em.getLightType();
            base.CreateLight();
		}
		base.Update();
	}
}
