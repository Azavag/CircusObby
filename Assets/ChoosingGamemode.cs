using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Gamemode
{
    normal,
    speedrun
}

public class ChoosingGamemode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startGameText;
    string continueGameText, newGameText;
    string ruContinueText = "Продолжить", ruNewGameText = "Новая игра";
    string enContinueText = "Continue", enNewGameText = "New game";
    [SerializeField] TextMeshProUGUI bestTimeText;
    [Header("Подсказки")]
    [SerializeField] GameObject normalGamemmodeHintObject;
    [SerializeField] GameObject speedrunGamemmodeHintObject;
    NavigationController navigationController;
    SoundController soundController;
    private void Awake()
    {
        navigationController = GetComponent<NavigationController>();
        soundController = FindObjectOfType<SoundController>();
        normalGamemmodeHintObject.SetActive(false);
        speedrunGamemmodeHintObject.SetActive(false);
    }
    void Start()
    {
        if (Language.Instance.currentLanguage == "ru")
        {
            continueGameText = ruContinueText;
            newGameText = ruNewGameText;
        }
        else
        {
            continueGameText = enContinueText;
            newGameText = enNewGameText;
        }
    }

    public void ChangeStartGameText()
    {
        if (Progress.Instance.playerInfo.spawnPointNumber > 0)
            startGameText.text = continueGameText;
        else startGameText.text = newGameText;
    }
    public void ChangeBestTimeText()
    {
        if (Progress.Instance.playerInfo.bestTimeSpeedrunMiliseconds > 0)
        {
            int miliseconds = Progress.Instance.playerInfo.bestTimeSpeedrunMiliseconds;
            int minutes = miliseconds / 60000;
            int seconds = (miliseconds % 60000) / 1000;
            string timeString = $"{minutes}:{seconds:00}";
            bestTimeText.text = timeString;
        }
        else bestTimeText.text = "-";
      
    }
    //По кнопке
    public void ChooseNormalGamemode()
    {
        navigationController.SwapChooseGamemodeToMainPanel();
        navigationController.StartNormalGamemode();
    }
    //По кнопке
    public void ChooseSpeedRunGamemode()
    {
        navigationController.SwapChooseGamemodeToMainPanel();
        navigationController.StartSpeedrunGamemode();
    }
    public void SwitchNormalGamemodeHint(bool state)
    {
        normalGamemmodeHintObject.SetActive(state);
        soundController.MakeClickSound();
    }
    public void SwitchSpeedrunGamemodeHint(bool state)
    {
        speedrunGamemmodeHintObject.SetActive(state);
        soundController.MakeClickSound();
    }

}
