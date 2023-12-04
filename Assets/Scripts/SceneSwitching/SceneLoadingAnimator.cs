using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingAnimator : MonoBehaviour
{
    [SerializeField] Animator transition;
    float transitionDuration = 0.6f;
    public static SceneLoadingAnimator instance;

    public void LoadNewScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }
    IEnumerator LoadSceneRoutine(string sceneName)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneName);
    }
}
