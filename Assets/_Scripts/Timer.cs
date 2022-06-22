using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    bool isRunning;
    public float timer;
    string time;
    public TMPro.TMP_Text uiTimer;

    public bool goesBackwards;
    public float startTime;
   
    public void StartTimer()
    {
        timer = startTime;
        isRunning = true;
        time = "00:00:000";
    }
    public void StopTimer()
    {
        isRunning = false;
    }
    public void ResetTimer()
    {
        timer = startTime;
        time = "00:00:000";
    }
    private void Update()
    {
        if (isRunning)
        {
            if (goesBackwards)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer += Time.deltaTime;
            }
            
            Clock();
            uiTimer.text = time;
        }
    }

    private void Clock()
    {
        int seconds = Mathf.FloorToInt(timer);
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds -= 60 * minutes;
        float miliseconds = timer - seconds - minutes * 60;
        miliseconds *= 1000;
        miliseconds = Mathf.FloorToInt(miliseconds);
        string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();

        string milisecondsString = miliseconds >= 100 ? miliseconds.ToString() : miliseconds < 10 ? $"00{miliseconds}" : $"0{miliseconds}";


        if (minutes >= 1)
        {
            time = $"{minutes}:{secondsString}:{milisecondsString}";
        }
        else
        {
            time = $"   {secondsString}:{milisecondsString}";
        }
    }
}
