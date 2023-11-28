using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject controllers, canvases, sdk, level, player, setup;
    [SerializeField] bool inEditor;
    [SerializeField] NavigationController navigationController;
    private void Awake()
    {
        sdk.SetActive(false);
        setup.SetActive(false);
        level.SetActive(false);
        player.SetActive(false);
        canvases.SetActive(false);
        controllers.SetActive(false);
    }
    private void Start()
    {
#if UNITY_EDITOR
        LoadGame();
#endif
    }
    public void LoadGame()
    {
        sdk.SetActive(true);
        controllers.SetActive(true);
        canvases.SetActive(true);
        level.SetActive(true);
        //player.SetActive(true);
        setup.SetActive(true);

        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(0.7f);
        if (LevelStartReciever.instance.canRunLevel)
        {
            navigationController.ShowGame();
        }
    }

}