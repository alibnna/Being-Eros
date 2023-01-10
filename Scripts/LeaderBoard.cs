using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public float verticalPositionPerPoint;
    public float offset;
    public int score;
    public List<int> scoreList;
    public TMP_Text text;
    public Vector3 pos;
    public Vector3 scale;
    public Vector3 temp;
    public static int newscore;
    public static List<int> scores;
    public static ScoreList lead = new ScoreList();
    private void Start()
    {

        scale =new Vector3(1, 1, 1);
        pos = parent.transform.position;
        pos.y += 1.3f;
        temp = pos;
        temp.x -= 0.5f;
        if(HeartAnim.score > 0)
        {
            lead.AddScore(HeartAnim.score);
        }
        
        upscor(lead.getScoreList());
    }

    public void upscor(List<int> scores)
    {
        bubbleSort(scores);
        for (int i = 0; i < 5; i++)
        {
            // Calculate the vertical position based on the score
            float y = scores[i] * verticalPositionPerPoint + offset;
            pos.y -= 0.3f;
            // Create the object at the calculated position
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
            obj.transform.SetParent(parent);
            obj.transform.localScale = scale;
            obj.GetComponent<TMP_Text>().text = scores[i].ToString();
            temp.y -= 0.3f;
            GameObject kobj = Instantiate(prefab, temp, Quaternion.identity);
            kobj.transform.SetParent(parent);
            kobj.transform.localScale = scale;
            kobj.GetComponent<TMP_Text>().text = (i + 1).ToString();
        }
    }

    static void bubbleSort(List<int> arr)
    {
        int n = arr.Count;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] < arr[j + 1])
                {
                    // swap temp and arr[i]
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
    }
}