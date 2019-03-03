using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalScorer : MonoBehaviour
{
    public int score;
    public Text scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScore (int amount)
    {
        this.score += amount;
        setScoreText();
    }

    void setScoreText()
    {
        scoreTxt.text = "Score: " + score.ToString();
    }
}
