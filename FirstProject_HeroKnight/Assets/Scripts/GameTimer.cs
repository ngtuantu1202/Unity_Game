using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;
    private float startTime;
    private float totalTime;
    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); 
    }

    public void StartTimer()
    {
        startTime = Time.time;
        gameEnded = false;
    }

    public void EndTimer()
    {
        if (!gameEnded)
        {
            totalTime = Time.time - startTime;
            gameEnded = true;
        }
    }

    public float GetTotalTime()
    {
        return totalTime;
    }
}
