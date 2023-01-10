using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void loadFirstScene()
    {
        SceneManager.LoadScene("FirstScene");
    }
    public void loadLeaderBoardScene()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
}
