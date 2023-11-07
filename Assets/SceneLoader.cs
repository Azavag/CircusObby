using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CheckSdkReady();

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


