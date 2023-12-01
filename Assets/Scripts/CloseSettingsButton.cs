using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButton : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] NavigationController navigationController;
    SoundController soundController;
    private void OnEnable()
    {
        navigationController = FindObjectOfType<NavigationController>();
        soundController = FindObjectOfType<SoundController>();
    }

    public void CloseSettings()
    {
        soundController.SaveVolumeSetting();
        navigationController.ShowSettingMenu();
    }

}
