using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score;
    public int target_score;
    public int arrow_num;
    void Start()
    {
        this.score = 0;
        target_score = 0;
        arrow_num = 10;
    }

    public void Record(GameObject target)
    {   
        // it must have this script
        // get the ring num and add it to current score
        if(target.GetComponent<RingData>()!= null)
        {
            score += target.GetComponent<RingData>().GetScore();
        }
        else
        {
            Debug.Log("得分不对阿");
        }
    }
    public void Record(int s)
    {
        score += s;
    }
    public void Reset()
    {
        score = 0;
    }
    // when we need score we can call this function to get score
    public int GetScore()
    {
        return score;
    }
}
