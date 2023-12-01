using UnityEngine;

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
    //По кнопке "Продолжить"
    public void SwitchSceneToMenu()
    {
        levelLoadAnimator.LoadNewScene("MainScene");
    }

}
