using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingData : MonoBehaviour
{
    int score;
    public int GetScore()
    {
        string name = this.gameObject.name;
        this.score = 6 - (name[0] - '0');
        Debug.Log("name?!"+name);
        return 6 - (name[0] - '0');
    }
}
