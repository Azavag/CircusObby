using System.Collections;
using UnityEngine;

public class MainSceneLoader : MonoBehaviour
{
    [SerializeField] GameObject controllers, canvases, sdk, level, player, setup;
    ProgressLoadedChecker progressLoadedChecker;
    bool isSceneLoaded;
    private void Awake()
    {
        level.SetActive(false);
        //player.SetActive(false);
        canvases.SetActive(false);
        controllers.SetActive(false);
        progressLoadedChecker = FindObjectOfType<ProgressLoadedChecker>();
    }

    IEnumerator LoadMainSceneRoutine()
    {
        yield return new WaitUntil(() => progressLoadedChecker.isProgressLoaded);
        LoadGame();
    }
    private void LateUpdate()
    {
        if (progressLoadedChecker.isProgressLoaded && !isSceneLoaded)
            LoadGame();
    }
    public void LoadGame()
    {
        controllers.SetActive(true);
        canvases.SetActive(true);
        level.SetActive(true);       
        isSceneLoaded = true;
    }
  
}
