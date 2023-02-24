using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{
    public static TimerControl Instance;

    public TextMeshProUGUI timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    public static string timerAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timeCounter.text = "00:00.00";
        timerGoing = false;

        timerAmount = "00:00.00";
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        
        StartCoroutine(UpdateTimer());

    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingstr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingstr;

            yield return null;  
        }
    }
}
