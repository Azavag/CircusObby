using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CheckSdkReady();

    private void Start()
    {
#if UNITY_EDITOR
        SwitchScene();
#endif
    }
    private void Update()
    {
#if !UNITY_EDITOR
        CheckSdkReady();
#endif
    }
    public void SwitchScene()
    {
        Debug.Log("SwitchScene");
        SceneManager.LoadScene("MainScene");
    }
}


