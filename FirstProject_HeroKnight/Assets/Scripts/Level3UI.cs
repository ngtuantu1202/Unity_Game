using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3UI : MonoBehaviour
{
    public Text textWinner;

    void Start()
    {
        if (GameTimer.instance != null)
        {
            GameTimer.instance.EndTimer();

            float time = GameTimer.instance.GetTotalTime();
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);

            textWinner.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            textWinner.text = "";
        }
    }
}
