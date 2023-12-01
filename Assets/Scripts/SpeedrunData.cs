using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedrunData : MonoBehaviour
{
    float mainTimer;
    bool timeIsGoing;
    int minutes;
    int seconds;
    int bestTimeInMiliseconds = int.MaxValue;
    void Start()
    {
        bestTimeInMiliseconds = Progress.Instance.playerInfo.bestTimeSpeedrunMiliseconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsGoing)
        {
            TimeProccess();           
        }
    }

    public void StartTimer()
    {
        Debug.Log("StartTimer");
        timeIsGoing = true;
    }
    void TimeProccess()
    {
        mainTimer += Time.deltaTime;
    }
    public void PauseTimer(bool state)
    {
        timeIsGoing = state;
    }
    public void EndTimer()
    {
        timeIsGoing = false;
        bestTimeInMiliseconds = (minutes * 60 * 1000) + (seconds * 1000);
        if (bestTimeInMiliseconds < Progress.Instance.playerInfo.bestTimeSpeedrunMiliseconds 
            || Progress.Instance.playerInfo.bestTimeSpeedrunMiliseconds < 0)
        {
            Progress.Instance.playerInfo.bestTimeSpeedrunMiliseconds = bestTimeInMiliseconds;
            YandexSDK.Save();
            YandexSDK.SetToLeaderboard();
        }
    }
    public void ResetTimer()
    {
        mainTimer = 0f;
    }

    public string ConvertTimeToFormat()
    {
        minutes = (int)(mainTimer / 60);
        seconds = (int)(mainTimer % 60);
        string timeString = $"{minutes}:{seconds:00}";
        return timeString;
    }
}
