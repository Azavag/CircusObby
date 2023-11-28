using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButton : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] NavigationController navigationController;
    private void OnEnable()
    {
        navigationController = FindObjectOfType<NavigationController>();
    }

    public void CloseSettings()
    {
        navigationController.ShowSettingMenu();
    }

}
