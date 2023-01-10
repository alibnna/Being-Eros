using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreList
{
    public static List<int> scores;

    public ScoreList()
    {
        scores = new List<int>();
    }

    public void AddScore(int score)
    {
        scores.Add(score);
    }

    public List<int> getScoreList()
    {
        return scores;
    }

}
