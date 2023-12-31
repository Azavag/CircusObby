using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedRunLevelController : MonoBehaviour
{
    [SerializeField] SpeedrunData speedRunData;
    [SerializeField] GameObject timerPlaceObject;
    [SerializeField] TextMeshProUGUI ingameTimerText;
    [SerializeField] GameObject leaderboardObject;
    [SerializeField] LeaderboardController leaderboardController;
    [Header("��������")]
    [SerializeField] GameObject startTrigger;
    [SerializeField] GameObject endTrigger;

    private void Update()
    {
        ShowCurrentTimerText();
    }
    public void SetupSpeedRun(bool state)
    {
        timerPlaceObject.SetActive(state);
        startTrigger.SetActive(state);
        endTrigger.SetActive(state);
        leaderboardObject.SetActive(state);
        leaderboardController.RecieveLeaderBoard();
        ShowCurrentTimerText();
    }

    void ShowCurrentTimerText()
    {
        ingameTimerText.text = speedRunData.ConvertTimeToFormat();
    }

    public void StartSpeedRun()
    {
        speedRunData.StartTimer();     
    }
    public void ToggleSpeedRunTimer(bool state)
    {
        speedRunData.PauseTimer(state);
    }

    public void EndSpeedRun()
    {
        speedRunData.EndTimer();
        ingameTimerText.text = speedRunData.ConvertTimeToFormat();
    }
    public void CancelSpeedRun()
    {
        speedRunData.CancelTimer();
    }
}
