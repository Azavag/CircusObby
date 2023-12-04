using UnityEngine;

public class CharacterSwapNavigation : MonoBehaviour
{
    [SerializeField] CharacterSwapper characterSwapper;
    SoundController soundController;
    SceneLoadingAnimator levelLoadAnimator;
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        levelLoadAnimator = FindObjectOfType<SceneLoadingAnimator>();
    }

    //�� ������ �������
    public void ChooseCharacter()
    {
        soundController.Play("CharacterChoosing");
        characterSwapper.SetChoosedStateToCharacter();
        characterSwapper.PlayChoosingAnimation();
        Progress.Instance.playerInfo.choosenCharacterNumber = characterSwapper.GetCurrentCharacterNumber();
        YandexSDK.Save();
    }

    public void OnLeftSlideButton()
    {
        soundController.Play("Select");
        characterSwapper.LeftHeroSlide();
    }
    public void OnRightSlideButton()
    {
        soundController.Play("Select");
        characterSwapper.RightHeroSlide();
    }
    //�� ������ "����������"
    public void SwitchSceneToMenu()
    {
        soundController.MakeClickSound();
        levelLoadAnimator.LoadNewScene("MainScene");
    }

}
