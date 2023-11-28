using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSwapNavigation : MonoBehaviour
{
    [SerializeField] CharacterSwapper characterSwapper;
    SoundController soundController;
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    //По кнопке выбрать
    public void ChooseCharacter()
    {
        soundController.Play("PositiveClick");
        characterSwapper.SetChoosedStateToCharacter();
        Progress.Instance.playerInfo.choosenCharacterNumber = characterSwapper.GetCurrentCharacterNumber();
        YandexSDK.Save();
    }

    public void OnLeftSlideButton()
    {
        characterSwapper.LeftHeroSlide();
    }
    public void OnRightSlideButton()
    {
        characterSwapper.RightHeroSlide();
    }

    public void SwitchSceneToGame()
    {
        LevelStartReciever.instance.canRunLevel = true;
        SceneManager.LoadScene("MainScene");        
    }
    public void SwitchSceneToMenu()
    {
        LevelStartReciever.instance.canRunLevel = false;
        SceneManager.LoadScene("MainScene");
    }

}
