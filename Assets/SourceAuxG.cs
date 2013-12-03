using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GlassScript))]

public class SourceAuxG : Source
{

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            base.lightDirD[i] = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    new void Update()
    {
        GlassScript em = this.GetComponent<GlassScript>();
        
        if (em.getIsLit())
        {
            Vector3[] ldir = new Vector3[4];
            ldir = em.getLightDir();

            for(int i = 0; i < 4; i++)
            {
                base.lightDirD[i] = ldir[i];
            }
            base.lightPos = em.getLightPos();

            base.lightType = 2;
            base.CreateDispersedLight();
        }

        else if (em.getIsLitOne())
        {
            base.lightDir = em.getLightDirOne();
            base.lightPos = em.getLightPos();

            base.lightType = em.getLightType();
            base.CreateLight();
        }
        base.Update();
    }
}