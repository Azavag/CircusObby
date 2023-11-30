using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSwapNavigation : MonoBehaviour
{
    [SerializeField] CharacterSwapper characterSwapper;
    SoundController soundController;
    LevelLoadAnimator levelLoadAnimator;
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        levelLoadAnimator = FindObjectOfType<LevelLoadAnimator>();
    }

    //По кнопке выбрать
    public void ChooseCharacter()
    {
        soundController.Play("PositiveClick");
        characterSwapper.SetChoosedStateToCharacter();
        characterSwapper.PlayChoosingAnimation();
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

    public void SwitchSceneToMenu()
    {
        levelLoadAnimator.LoadNewScene("MainScene");
    }

}
