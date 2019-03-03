using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorer : MonoBehaviour
{
    public Text scoreTxt;
    private int score = 0;
    //  private int remainingObjs = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject wall = GameObject.Find("wall");
        int score = 0;
        if (GameObject.Find("Sphere").GetComponent<randomizeObj>().Getptag() == "garbage") {
            score = 1;
        }
        else
        {
            score = -1;
        }
        wall.GetComponent<totalScorer>().changeScore(score);
        }

    void setScoreText()
    {
        scoreTxt.text = "Score: " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
