using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{

    public bool isHit = false;
    public int targetColour;

    // Use this for initialization
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {

            // used for debug purposes
        } 

        // test function to rotate the mirrors in-game
        if (Input.GetKeyDown("d"))
            transform.Translate(0.5f, 0.0f, 0.0f);
        if (Input.GetKeyDown("a"))
            transform.Translate(-0.5f, 0.0f, 0.0f);
        if (Input.GetKeyDown("w"))
            transform.Translate(0.0f, 0.0f, 0.5f);
        if (Input.GetKeyDown("s"))
            transform.Translate(0.0f, 0.0f, -0.5f);

    }

    public void Hit()
    {
        isHit = true;
    }

    public void setColour(int i)
    {
        targetColour = i;
    }

    public int getColour()
    {
        return targetColour;
    }
}
