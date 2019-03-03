using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeObj : MonoBehaviour
{
    public Material[] allObjMats = new Material  [6];
    private string ptag;
    private int lastRand;

 
    // Start is called before the first frame update
    void Start()
    {
        generateANewTexture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateANewTexture ()
    {
        lastRand = Random.Range(0, allObjMats.Length);
        GetComponent<Renderer>().material = allObjMats[lastRand];
        SetTag();
    }

    void SetTag () //This is hard coded for the materials
    {
        if (lastRand == 1|| lastRand == 3)
        {
            ptag = "recycling";
        }
        else if (lastRand == 2 || lastRand == 5)
        {
            ptag = "garbage";
        }
        else
        {
            ptag = "compost";
        }
    }

    public string Getptag ()
    {
        return ptag;
    }
}
