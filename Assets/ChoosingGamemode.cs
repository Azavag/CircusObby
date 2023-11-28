using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoosingGamemode : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI startGameText;
    string continueGameText, newGameText;
    string ruContinueText = "Продолжить", ruNewGameText = "Новая игра";
    string enContinueText = "Continue", enNewGameText = "New game";
    // Start is called before the first frame update
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
        ChangeStartGameText();
    }

    void ChangeStartGameText()
    {
        //if (Progress.Instance.playerInfo.spawnPointNumber > 0)
        //    startGameText.text = continueGameText;
        //else startGameText.text = newGameText;
    }

    public void ChooseNormalGamemode()
    {

    }

    public void ChooseTimerSpeedRunGamemode()
    {

    }
}
