using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorerCo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject wall = GameObject.Find("wall");
        int score = 0;
        if (GameObject.Find("Sphere").GetComponent<randomizeObj>().Getptag() == "compost")
        {
            score = 1;
        }
        else
        {
            score = - 1;
        }
        wall.GetComponent<totalScorer>().changeScore(score);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
